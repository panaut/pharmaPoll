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

        ServiceResponse<string> GetSurvey(int surveyId, string culture = "DEFAULT", bool onlyActive = true);

        ServiceResponse<string> GetSurvey(string surveyCode, string culture = "DEFAULT", bool onlyActive = true);

        ServiceResponse<IEnumerable<Survey>> GetAllSurveys(bool onlyActive = true);

        ServiceResponse DeleteSurvey(int surveyId);

        ServiceResponse<byte[]> GetCsvLocalizations(int surveyId, bool doNotExtractAgain = false);

        ServiceResponse UpdateLocalizationFromCSVs(int surveyId, byte[] csvBytes);

        ServiceResponse DeleteLocalizations(int surveyId);

    }
}
