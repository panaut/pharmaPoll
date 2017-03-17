//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using PollQuestionnaire.UI.Web.Serialization;
//using Questionnaire.DataBroker.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Questionnaire.DataBroker.Serialization
//{
//    public class SurveyConverter : JsonConverter
//    {
//        public override bool CanConvert(Type objectType)
//        {
//            return true;
//        }

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {

//            //var retObj = JsonConvert.DeserializeObject(JObject.Load(reader).ToString(), type: objectType, settings: this.SerializerSettings);

//            return JObject.Load(reader).ToString();
//        }

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            JsonConvert.SerializeObject(value, settings: this.SerializerSettings);
//        }

//        private JsonSerializerSettings SerializerSettings
//        {
//            get
//            {
//                return new JsonSerializerSettings
//                {
//                    TypeNameHandling = TypeNameHandling.All,
//                    Binder = new KnownTypesBinder
//                    {
//                        KnownTypes = new List<Type> {
//                        typeof(QuestionCheckBox),
//                        typeof(QuestionRadiogroup),
//                        typeof(QuestionDropdown),
//                        typeof(QuestionText),
//                        typeof(QuestionComment),
//                        typeof(QuestionRating),
//                        typeof(QuestionHtml),
//                        typeof(QuestionFile),
//                        typeof(QuestionMatrix),
//                        typeof(QuestionMatrixDropdown) }
//                    }
//                };
//            }
//        }
//    }
//}
