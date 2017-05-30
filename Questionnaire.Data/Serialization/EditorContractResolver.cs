using Newtonsoft.Json.Serialization;
using System;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using Questionnaire.Data.Model;

namespace Questionnaire.Data.Serialization
{
    public class EditorContractResolver : DefaultContractResolver
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override JsonContract ResolveContract(Type type)
        {
            var contract = base.ResolveContract(type);

            return contract;
        }

        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(IEnumerable<IValueTextPair>).IsAssignableFrom(objectType))
            {
                return new ValueTextPairConverter();
            }
            else
            {
                return base.ResolveContractConverter(objectType);
            }

            //if (converter != null)
            //    logger.Info($"Type {objectType.GetType().Name} was assigned with converter {converter.GetType().Name}");
            //else
            //    logger.Info($"Type {objectType.GetType().Name} was not assigned with converter");
        }
    }
}
