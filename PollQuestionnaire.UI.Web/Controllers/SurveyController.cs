using Newtonsoft.Json;
using Questionnaire.Service;
using Questionnaire.Service.Objects;
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
        public ActionResult PatQueSurvey(
                        string surveyCode,
                        string sessionCode = null)
        {
            var surveyInfoResult = this.surveyService.Value.GetSurveyInfo(surveyCode);

            if (surveyInfoResult.Status != OperationStatus.Success)
            {
                throw new Exception($"Survey with code: {surveyCode} wasn't found");
            }

            ViewBag.codeSurveyId = HttpUtility.HtmlEncode(surveyInfoResult.OperationResult.Code);
            ViewBag.surveyName = HttpUtility.HtmlEncode(surveyInfoResult.OperationResult.Title);
            ViewBag.sessionCode = HttpUtility.HtmlEncode(sessionCode);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public string SaveResults(
            int surveyId,
            string jsonResults,
            string sessionCode = default(string),
            int? lastVisitedPageIndex = default(int?),
            bool isFinal = true)
        {
            var clientIp = Request.UserHostAddress;

            var result = votingService.Value
                .SaveSession(
                surveyId, clientIp, jsonResults, lastVisitedPageIndex, sessionCode, isFinal);

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception("Failed to store results");
            }

            return result.OperationResult;
        }

        [HttpGet()]
        public string GetActiveSurvey(string surveyCode)
        {
            // Now retrieve the survey
            var surveyResult = this.surveyService.Value.GetSurvey(surveyCode);

            if (surveyResult.Status != OperationStatus.Success)
            {
                throw new Exception($"Survey with code: {surveyCode} wasn't found");
            }

            return surveyResult.OperationResult;
        }

        [HttpGet()]
        public string GetActiveSession(string surveyCode, string sessionCode)
        {
            // Retrieve the session
            var sessionResult = this.votingService.Value.GetSession(surveyCode, sessionCode);

            if (sessionResult.Status != OperationStatus.Success)
            {
                throw new Exception("Failed to retrieve voting session");
            }

            if (sessionResult.OperationResult == null)
            {
                throw new Exception("Session doesn't exist or it is inactive.");
            }

            return JsonConvert.SerializeObject(sessionResult.OperationResult);
        }
    }
}