using Newtonsoft.Json;
using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;
using Questionnaire.Serialization.Model;
using System;
using System.Collections.Generic;

namespace Questionnaire.Data.Serialization
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
                        typeof(CheckBox),
                        typeof(Radiogroup),
                        typeof(Dropdown),
                        typeof(Text),
                        typeof(Comment),
                        typeof(Rating),
                        typeof(Html),
                        typeof(File),
                        typeof(PlainMatrix),
                        typeof(ComplexMatrix),
                        typeof(ComplexMatrixDynamic),
                        typeof(MultipleText)
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

        public string Serialize(IEnumerable<Survey> surveys)
        {
            return JsonConvert.SerializeObject(surveys);
        }
    }
}
