using Newtonsoft.Json;
using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;
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
                        typeof(MultipleText),
                        typeof(Panel)
                        }
                    };

                    return new JsonSerializerSettings
                    {
                        Binder = knownTypesBinder,
                        Formatting = Formatting.None
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
            return this.SerializeObject(survey);
        }

        public string Serialize(IEnumerable<Survey> surveys)
        {
            return this.SerializeObject(surveys);
        }

        private string SerializeObject(object obj)
        {
            string objJson = string.Empty;

            var serializer = JsonSerializer.CreateDefault();

            using (var stringWriter = new System.IO.StringWriter())
            {
                using (var writer = new JsonTextWriter(stringWriter))
                {
                    writer.QuoteName = true;
                    serializer.Serialize(writer, obj);
                }

                objJson = stringWriter.ToString();
            }

            return objJson;
        }
    }
}
