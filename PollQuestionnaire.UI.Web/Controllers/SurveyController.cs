using Questionnaire.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PollQuestionnaire.UI.Web.Controllers
{
    public class SurveyController : Controller
    {
        private Lazy<ISurveyService> surveyService = new Lazy<ISurveyService>(() => new SurveyService());
        private Lazy<IVotingService> votingService = new Lazy<IVotingService>(() => new VotingService());

        [Authorize]
        // GET: SurveyDemo
        public ActionResult Index()
        {
            var model = surveyService.Value.GetAllSurveyInfo().OperationResult;
            return View(model.ToList());
        }

        [AllowAnonymous]
        public ActionResult PatQueSurvey(string surveyId)
        {
            var result = this.surveyService.Value.GetSurveyInfo(surveyId);

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception($"Survey with code: {surveyId} wasn't found");
            }

            ViewBag.codeSurveyId = HttpUtility.HtmlEncode(result.OperationResult.Code);
            ViewBag.surveyName = HttpUtility.HtmlEncode(result.OperationResult.Title);

            var fileName = surveyId + "_en-US.html";
            var filePath = "~/Concents/";
            var fullPath = filePath + fileName;
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(fullPath));
            ViewBag.concent = HttpUtility.HtmlDecode(fileContents);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public string SaveResults(int surveyId, string jsonResults)
        {
            var clientIp = Request.UserHostAddress;

            var result = votingService.Value
                .InitializeSession(surveyId, clientIp, jsonResults, isFinal: true);

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception("Failed to store results");
            }

            return result.OperationResult;
        }

        [HttpGet()]
        public string GetActiveSurvey(
                        string surveyCode, 
                        string localizationCulture = null,
                        string sessionCode = null)
        {
            var result = surveyService.Value.GetSurvey(surveyCode);

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception("Could not fetch the survey with surveyId=" + surveyCode, null);
            }

            return result.OperationResult;
        }
    }
}