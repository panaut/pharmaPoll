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
            var model = surveyService.Value.GetAllSurveys().OperationResult;

            return View(model);
        }

        [HttpPost()]
        public bool ActivateSurvey(int surveyId)
        {
            var result = surveyService.Value.SetSurveyStatus(surveyId, true);

            if(result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("failed to update survey status");
            }

            return result.OperationResult.Value;
        }

        [HttpPost()]
        public bool DeactivateSurvey(int surveyId)
        {
            var result = surveyService.Value.SetSurveyStatus(surveyId, false);

            if (result.Status != OperationStatus.Success)
            {
                throw new InvalidOperationException("failed to update survey status");
            }

            return result.OperationResult.Value;
        }
    }
}