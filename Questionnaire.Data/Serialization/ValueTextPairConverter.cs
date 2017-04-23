using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questionnaire.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Questionnaire.Data.Serialization
{
    internal class ValueTextPairConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var retVal = new object();

            if (reader.TokenType == JsonToken.StartArray && typeof(IEnumerable<IValueTextPair>).IsAssignableFrom(objectType))
            {
                Type innerType = objectType.GetGenericArguments()[0];

                // var retObj = new List<ValueTextPair>();

                Type genericListType = typeof(List<>).MakeGenericType(innerType);
                var retObj = (IList)Activator.CreateInstance(genericListType);

                JArray ja = JArray.Load(reader);

                foreach (var item in ja.Children())
                {
                    if (item.Type == JTokenType.Object)
                    {
                        using (StringReader sr = new StringReader(item.ToString()))
                        using (JsonTextReader jtr = new JsonTextReader(sr))
                        {
                            var detail = Convert.ChangeType(serializer.Deserialize(jtr, innerType), innerType);
                            retObj.Add(detail);
                        }
                    }
                    else
                    {
                        //var detail = new ValueTextPair { value = item.ToString() };
                        var detail = (IValueTextPair)Convert.ChangeType(Activator.CreateInstance(innerType), innerType);
                        detail.value = item.ToString();
                        retObj.Add(detail);
                    }
                }

                retVal = retObj;
            }

            return retVal;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IEnumerable<IValueTextPair> details = value as IEnumerable<IValueTextPair>;

            if (details != null)
            {
                writer.WriteStartArray();

                foreach (var item in details)
                {
                    WriteJson(writer, item, serializer);
                }

                writer.WriteEndArray();
            }
            else
            {
                if (value is IValueTextPair && string.IsNullOrEmpty(((IValueTextPair)value).text))
                {
                    writer.WriteValue(((IValueTextPair)value).value);
                }
                else
                {
                    serializer.Serialize(writer, value, value.GetType());
                };
            }
        }
    }
}