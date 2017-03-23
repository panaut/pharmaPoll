using Newtonsoft.Json;
using Questionnaire.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PollQuestionnaire.UI.Web.Controllers
{
    public class SurveyEditorController : Controller
    {
        private Lazy<ISurveyService> surveyService = new Lazy<ISurveyService>(() => new SurveyService());

        // GET: SurveyEditor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public int SaveSurvey(string surveyJson)
        {
            var result = surveyService.Value.CreateOrSaveSurvey(surveyJson);

            if(result.Status != OperationStatus.Success)
            {
                throw new Exception();
            }

            return result.OperationResult;
        }
    }
}