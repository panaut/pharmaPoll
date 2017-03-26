using Questionnaire.Data;
using Questionnaire.Service.Infrastructure;
using System.Collections.Generic;

namespace Questionnaire.Service
{
    public interface ISurveyService
    {
        ServiceResponse<int> CreateSurvey(string surveyJson);

        ServiceResponse<int> CreateOrSaveSurvey(string surveyJson);

        ServiceResponse<string> GetActiveSurvey();

        ServiceResponse<IEnumerable<Survey>> GetAllSurveys();
    }
}
