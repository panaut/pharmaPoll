using System;
using System.Collections.Generic;
using Questionnaire.Serialization.Model;
using Newtonsoft.Json;
using PollQuestionnaire.UI.Web.Serialization;

namespace Questionnaire.Serialization
{
    public class SurveySerializer : ISurveySerializer
    {
        public SurveySerializer()
        {
            if (JsonConvert.DefaultSettings == null)
            {
                JsonConvert.DefaultSettings = () =>
                {
                    KnownTypesBinder knownTypesBinder = new KnownTypesBinder
                    {
                        KnownTypes = new List<Type> {
                        typeof(Survey),
                        typeof(Page),
                        typeof(QuestionCheckBox),
                        typeof(QuestionRadiogroup),
                        typeof(QuestionDropdown),
                        typeof(QuestionText),
                        typeof(QuestionComment),
                        typeof(QuestionRating),
                        typeof(QuestionHtml),
                        typeof(QuestionFile),
                        typeof(QuestionMatrix),
                        typeof(QuestionMatrixDropdown),
                        typeof(QuestionMatrixDynamic),
                        typeof(QuestionMultipleText)
                        }
                    };

                    return new JsonSerializerSettings
                    {
                        Binder = knownTypesBinder
                    };
                };
            }
        }

        public Survey Deserialize(string surveyJson)
        {
            return JsonConvert.DeserializeObject<Survey>(surveyJson);
        }

        public string Serialize(Survey survey)
        {
            return JsonConvert.SerializeObject(survey);
        }

        public string Serialize(IEnumerable<Data.Survey> surveys)
        {
            return JsonConvert.SerializeObject(surveys);
        }
    }
}
