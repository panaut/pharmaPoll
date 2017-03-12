using Questionnaire.DataBroker.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace PollQuestionnaire.UI.Web.Serialization
{
    public class KnownTypesBinder : SerializationBinder
    {
        private IList<Type> knownTypes;
        public IList<Type> KnownTypes
        {
            get { return this.knownTypes; }
            set
            {
                this.knownTypes = value;
                this.typeMap = this.knownTypes
                    .Where(kt => kt.GetCustomAttribute(typeof(SurveyType)) != null)
                    .ToDictionary(kt => kt.GetCustomAttribute<SurveyType>().TypeIdentifier.ToLower(), kt => kt);
            }
        }

        private IDictionary<string, Type> typeMap;

        public override Type BindToType(string assemblyName, string typeName)
        {
            return typeMap.ContainsKey(typeName.ToLower()) ? typeMap[typeName.ToLower()] : null;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}