using Questionnaire.Data.Model;
using Questionnaire.Service.Infrastructure;
using Questionnaire.Service.Objects;
using System.Collections.Generic;

namespace Questionnaire.Service
{
    public interface ISurveyService
    {
        ServiceResponse<int> CreateSurvey(string surveyJson);

        ServiceResponse<int> CreateOrSaveSurvey(string surveyJson);

        ServiceResponse<bool?> SetSurveyStatus(int surveyId, bool status);

        ServiceResponse<string> GetSurveyStats(int surveyId, string culture = "DEFAULT", bool onlyActive = true);
        ServiceResponse<string> GetSurvey(int surveyId, string culture = "DEFAULT", bool onlyActive = true);

        ServiceResponse<string> GetSurvey(string surveyCode, string culture = "DEFAULT", bool onlyActive = true);

        ServiceResponse<SurveyInfo> GetSurveyInfo(string surveyCode, bool onlyActive = true);

        ServiceResponse<IEnumerable<SurveyInfo>> GetAllSurveyInfo(bool onlyActive = true);

        ServiceResponse DeleteSurvey(int surveyId);

        ServiceResponse<byte[]> GetCsvLocalizations(int surveyId, bool doNotExtractAgain = false);

        ServiceResponse UpdateLocalizationFromCSVs(int surveyId, byte[] csvBytes);

        ServiceResponse DeleteLocalizations(int surveyId);

        ServiceResponse<IEnumerable<ECulture>> GetSupportedLanguages(int surveyId);
    }
}
