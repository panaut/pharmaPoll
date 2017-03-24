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

        // GET: SurveyDemo
        [HttpGet()]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public string GetActiveSurvey()
        {
            var result = surveyService.Value.GetActiveSurvey();

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception();
            }

            return result.OperationResult;
        }
    }
}