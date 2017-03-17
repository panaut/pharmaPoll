using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questionnaire.DataBroker.Model;
using System;
using System.Collections.Generic;
using System.IO;

public class ValueTextPairConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var retVal = new object();

        if (reader.TokenType == JsonToken.StartArray && typeof(IEnumerable<ValueTextPair>).IsAssignableFrom(objectType))
        {
            var retObj = new List<ValueTextPair>();

            JArray ja = JArray.Load(reader);

            foreach (var item in ja.Children())
            {
                if (item.Type == JTokenType.Object)
                {
                    using (StringReader sr = new StringReader(item.ToString()))
                    using (JsonTextReader jtr = new JsonTextReader(sr))
                    {
                        var detail = serializer.Deserialize<ValueTextPair>(jtr);
                        retObj.Add(detail);
                    }
                }
                else
                {
                    var detail = new ValueTextPair { value = item.ToString() };
                    retObj.Add(detail);
                }
            }

            retVal = retObj;
        }

        return retVal;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        IEnumerable<ValueTextPair> details = value as IEnumerable<ValueTextPair>;

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
            if (value is ValueTextPair && string.IsNullOrEmpty(((ValueTextPair)value).text))
            {
                writer.WriteValue(((ValueTextPair)value).value);
            }
            else
            {
                serializer.Serialize(writer, value, value.GetType());
            };
        }
    }
}