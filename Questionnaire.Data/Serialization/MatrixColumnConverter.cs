using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questionnaire.Data.Model.QuestionDefinition;
using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Questionnaire.Data.Serialization
{
    internal class MatrixColumnConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var knownTypesBinder = serializer.Binder as KnownTypesBinder;

            var retVal = new object();

            if (reader.TokenType == JsonToken.StartArray && typeof(IEnumerable<QuestionBase>).IsAssignableFrom(objectType))
            {
                var retObj = new List<QuestionBase>();

                JArray ja = JArray.Load(reader);

                foreach (var matrixColumn in ja.Children())
                {
                    if (matrixColumn.Type == JTokenType.Object)
                    {
                        // Get Type Property from "$type" field...
                        var jObj = JObject.Parse(matrixColumn.ToString());
                        var strType = jObj["cellType"]?.Value<string>();

                        strType = strType ?? "dropdown";

                        Type targetType = knownTypesBinder.BindToType(string.Empty, strType);

                        targetType = targetType ?? typeof(QuestionBase);

                        var typedColumn = (QuestionBase)JsonConvert.DeserializeObject(
                                        matrixColumn.ToString(),
                                        targetType,
                                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

                        retObj.Add(typedColumn);
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
            var knownTypesBinder = serializer.Binder as KnownTypesBinder;

            IEnumerable<QuestionBase> matrixColumns = value as IEnumerable<QuestionBase>;

            if (matrixColumns != null)
            {
                writer.WriteStartArray();

                foreach (var item in matrixColumns)
                {
                    JObject jo = JObject.FromObject(item);

                    string assemblyName = string.Empty;
                    string typeName = string.Empty;

                    knownTypesBinder.BindToName(item.GetType(), out assemblyName, out typeName);

                    jo.Add("cellType", typeName);

                    serializer.Serialize(writer, jo);

                    //writer.WriteRaw(jo.ToString());
                }

                writer.WriteEndArray();
            }
        }
    }
}