using Questionnaire.Data.Model;
using System.Collections.Generic;

namespace Questionnaire.Data.Serialization
{
    public interface ISurveySerializer
    {
        string Serialize(Survey survey);

        Survey Deserialize(string surveyJson);

        string Serialize(IEnumerable<Survey> surveys);
    }
}
