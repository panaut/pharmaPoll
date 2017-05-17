using Questionnaire.Service;
using System;
using System.Collections.Generic;
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
            var model = surveyService.Value.GetAllSurveys().OperationResult;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PatQueSurvey(string surveyId, string surveyName)
        {
            ViewBag.codeSurveyId = HttpUtility.HtmlEncode(surveyId);
            ViewBag.surveyName = HttpUtility.HtmlEncode(surveyName);
            //string codeSurveyId = Request.QueryString["surveyId"];
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public string SaveResults(int surveyId, string jsonResults)
        {
            var clientIp = Request.UserHostAddress;

            var result = votingService.Value
                .InitializeSession(surveyId, clientIp, jsonResults, isFinal: true);

            if(result.Status != OperationStatus.Success)
            {
                throw new Exception("Failed to store results");
            }

            return result.OperationResult;
        }

        //// GET: SurveyDemo
        //[HttpGet()]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet()]
        public string GetActiveSurvey(string surveyCode)
        {
            // ToDo: Implement retrieval of a survey based on surveyId field (a unique non-id survey identifier e.g. F45RA)
            var result = surveyService.Value.GetSurvey(surveyCode);

            if (result.Status != OperationStatus.Success && result.Status != OperationStatus.Denied)
            {
                throw new Exception("Could not fetch the survey with surveyId=" + surveyCode, null);
            }
            return result.OperationResult;
        }
    }
}