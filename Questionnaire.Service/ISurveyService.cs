using Questionnaire.Data.Model;
using Questionnaire.Service.Infrastructure;
using System.Collections.Generic;

namespace Questionnaire.Service
{
    public interface ISurveyService
    {
        ServiceResponse<int> CreateSurvey(string surveyJson);

        ServiceResponse<int> CreateOrSaveSurvey(string surveyJson);

        ServiceResponse<bool?> SetSurveyStatus(int surveyId, bool status);

        ServiceResponse<string> GetActiveSurvey();

        ServiceResponse<string> GetSurvey(int surveyId);

        ServiceResponse<IEnumerable<Survey>> GetAllSurveys();
    }
}
