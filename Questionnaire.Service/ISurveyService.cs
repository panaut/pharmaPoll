using Questionnaire.Service.Infrastructure;

namespace Questionnaire.Service
{
    public interface ISurveyService
    {
        ServiceResponse<int> CreateSurvey(string surveyJson);

        ServiceResponse<int> CreateOrSaveSurvey(string surveyJson);

        ServiceResponse<string> GetActiveSurvey();

        ServiceResponse<string> GetAllSurveys();
    }
}
