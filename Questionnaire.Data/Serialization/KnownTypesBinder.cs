using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Questionnaire.Data.Serialization
{
    internal class KnownTypesBinder : SerializationBinder
    {
        private IEnumerable<Tuple<Attribute, Type>> knownTypesWithSurveyAttribute;
        public IEnumerable<Type> KnownTypes
        {
            get { return this.knownTypesWithSurveyAttribute.Select(tup => tup.Item2); }
            set
            {
                this.knownTypesWithSurveyAttribute = value
                    .Where(kt => kt.GetCustomAttribute<SurveyType>() != null)
                    .Select(kt => new Tuple<Attribute, Type>(kt.GetCustomAttribute<SurveyType>(), kt));
            }
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            return this.knownTypesWithSurveyAttribute.SingleOrDefault(tup => tup.Item1.Match(typeName))?.Item2;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            var attribute = this.knownTypesWithSurveyAttribute.SingleOrDefault(tup => tup.Item2.Equals(serializedType))?.Item1;

            typeName = attribute != null ? ((SurveyType)attribute).TypeIdentifier : null;
        }
    }
}