using Questionnaire.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Extensions
{
    public static class SurveyExtensions
    {
        public static Survey GenerateUniqueId(this Survey survey, int lenght)
        {
            if (survey.surveyId == null)
            {
                survey.surveyId = string.Join("", Guid.NewGuid().ToString("n").Take(lenght).Select(o => o)).ToUpper();
            }

            return survey;
        }
        public static Survey GenerateTitle(this Survey survey)
        {
            if (string.IsNullOrEmpty(survey.title))
            {
                survey.title = $"pqSurvey {DateTime.Now.ToString("yy-MM-dd HH:mm:ss")}";
            }

            return survey;
        }
    }
}
