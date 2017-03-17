using Newtonsoft.Json;
using Questionnaire.DataBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PollQuestionnaire.UI.Web.Controllers
{
    public class SurveyEditorController : Controller
    {
        // GET: SurveyEditor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public void SaveSurvey(string surveyJson)
        {
            var survey = JsonConvert.DeserializeObject<Survey>(surveyJson);

            var json = JsonConvert.SerializeObject(survey);
        }
    }
}