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

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception("Could not fetch the survey with surveyId=" + surveyCode, null);
            }

            return result.OperationResult;
        }
    }
}