using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questionnaire.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data.Serialization
{
    internal class PageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var retVal = new object();

            if (reader.TokenType == JsonToken.StartArray && typeof(IEnumerable<ElementBase>).IsAssignableFrom(objectType))
            {
                var retObj = new List<ElementBase>();

                JArray ja = JArray.Load(reader);

                foreach (var page in ja.Children())
                {
                    if (page.Type == JTokenType.Object)
                    {
                        // Get Type Property from "$type" field...
                        var jObj = JObject.Parse(page.ToString());

                        var typedPage = (Page)JsonConvert.DeserializeObject(
                                        page.ToString(),
                                        typeof(Page),
                                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

                        retObj.Add(typedPage);
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
                throw new JsonReaderException("Unexpected Json Token. Array of Matrix Columns is expected.");
            }

            return retVal;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var elements = value as IEnumerable<ElementBase>;

            IEnumerable<Page> pages = elements.OfType<Page>();

            if (pages != null)
            {
                writer.WriteStartArray();

                foreach (var item in pages)
                {
                    serializer.Serialize(writer, item);
                }

                writer.WriteEndArray();
            }
        }
    }
}