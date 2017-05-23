using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Questionnaire.Data.Serialization
{
    public class StringObjectsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var retObj = new object();

            if (reader.TokenType == JsonToken.StartArray && objectType == typeof(string))
            {
                var retVal = new List<string>();

                JArray ja = JArray.Load(reader);

                foreach (var item in ja.Children())
                {
                    retVal.Add(item.ToString());
                }

                retObj = String.Join("; ", retVal);
            }

            return retObj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var strJoin = value as String;

            if (!string.IsNullOrEmpty(strJoin))
            {
                var items = strJoin.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                writer.WriteStartArray();

                foreach (var item in items)
                {
                    serializer.Serialize(writer, item, item.GetType());
                }

                writer.WriteEndArray();
            }
        }
    }
}
