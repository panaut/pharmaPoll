using Questionnaire.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PollQuestionnaire.UI.Web.Controllers
{
    public class AdminController : Controller
    {
        private Lazy<ISurveyService> surveyService = new Lazy<ISurveyService>(() => new SurveyService());

        // GET: Admin
        public ActionResult Index()
        {
            var model = GetAllSurveys();

            return View(model);
        }
        [HttpGet()]
        public string GetAllSurveys()
        {
            var result = surveyService.Value.GetAllSurveys();

            if (result.Status != OperationStatus.Success)
            {
                throw new Exception();
            }

            return result.OperationResult;
        }
    }
}