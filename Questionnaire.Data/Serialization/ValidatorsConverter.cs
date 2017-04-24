using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questionnaire.Data.Model.QuestionDefinition;
using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Questionnaire.Data.Serialization
{
    internal class ValidatorsConverter : JsonConverter
    {
        private IEnumerable<Tuple<Attribute, Type>> knownTypesWithSurveyAttribute;

        private IEnumerable<Type> KnownTypes
        {
            get { return this.knownTypesWithSurveyAttribute.Select(tup => tup.Item2); }
            set
            {
                this.knownTypesWithSurveyAttribute = value
                    .Where(kt => kt.GetCustomAttribute<SurveyType>() != null)
                    .Select(kt => new Tuple<Attribute, Type>(kt.GetCustomAttribute<SurveyType>(), kt));
            }
        }

        private Type GetMatchingType(string type)
        {
            return this.knownTypesWithSurveyAttribute.SingleOrDefault(tup => tup.Item1.Match(type))?.Item2;
        }

        private string GetMatchingType(Type type)
        {
            string retVal = string.Empty;

            var att = this.knownTypesWithSurveyAttribute.SingleOrDefault(tup => tup.Item2.Equals(type))?.Item1;

            if (att != null)
            {
                retVal = ((SurveyType)att).TypeIdentifier;
            }

            return retVal;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var retVal = new object();

            if (reader.TokenType == JsonToken.StartArray && typeof(IEnumerable<SurveyValidator>).IsAssignableFrom(objectType))
            {
                var retObj = new List<SurveyValidator>();

                JArray ja = JArray.Load(reader);

                foreach (var validator in ja.Children())
                {
                    if (validator.Type == JTokenType.Object)
                    {
                        // Get Type Property from "$type" field...
                        var jObj = JObject.Parse(validator.ToString());
                        var strType = jObj["$type"].Value<string>();

                        Type targetType = this.GetMatchingType(strType);

                        targetType = targetType ?? typeof(SurveyValidator);

                        var typedValidator = (SurveyValidator)JsonConvert.DeserializeObject(
                                        validator.ToString(),
                                        targetType,
                                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

                        retObj.Add(typedValidator);
                    }
                    else
                    {
                        throw new JsonReaderException("Unexpected Json Token. Object was expected.");
                    }
                }

                retVal = retObj;
            }
            else
            {
                throw new JsonReaderException("Unexpected Json Token. Array of validators is expected.");
            }

            return retVal;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IEnumerable<SurveyValidator> validators = value as IEnumerable<SurveyValidator>;

            if (validators != null)
            {
                writer.WriteStartArray();

                foreach (var item in validators)
                {
                    // We shall need a custom instance of binder for this
                    var defaultBinder = serializer.Binder;

                    serializer.TypeNameHandling = TypeNameHandling.Objects;
                    serializer.Binder = new KnownTypesBinder { KnownTypes = this.KnownTypes };
                    serializer.Serialize(writer, item, item.GetType());

                    serializer.Binder = defaultBinder;
                }

                writer.WriteEndArray();
            }
        }

        public ValidatorsConverter()
        {
            this.KnownTypes = new List<Type> {
            typeof(NumericValidator),
            typeof(TextValidator),
            typeof(AnswerCountValidator),
            typeof(RegexValidator),
            typeof(EmailValidator)};
        }
    }
}