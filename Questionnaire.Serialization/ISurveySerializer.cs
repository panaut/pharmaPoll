using Questionnaire.Serialization.Model;

namespace Questionnaire.Serialization
{
    public interface ISurveySerializer
    {
        string Serialize(Survey survey);

        Survey Deserialize(string surveyJson);
    }
}
