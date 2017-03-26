using Questionnaire.Serialization.Model;
using System.Collections.Generic;

namespace Questionnaire.Serialization
{
    public interface ISurveySerializer
    {
        string Serialize(Survey survey);

        Survey Deserialize(string surveyJson);

        string Serialize(IEnumerable<Data.Survey> surveys);
    }
}
