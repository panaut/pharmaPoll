using Questionnaire.Data;
using Questionnaire.Data.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Extensions
{
    internal class LocalizationVisitorBase : VisitorBase
    {
        protected ILocalizationManager locManager;

        protected static ConcurrentDictionary<Type, IDictionary<PropertyInfo, string>> LocalizablePropertiesMap
            = new ConcurrentDictionary<Type, IDictionary<PropertyInfo, string>>();

        protected static ConcurrentDictionary<Type, string> TypeIdentifiersMap
            = new ConcurrentDictionary<Type, string>();

        public LocalizationVisitorBase(ILocalizationManager localizationManager)
        {
            this.locManager = localizationManager;
        }

        protected void UpdateMapping(Type type)
        {
            // Do we have this type mapped?
            var typeIdentifier = TypeIdentifiersMap.ContainsKey(type)
                ? TypeIdentifiersMap[type] : null;

            // If not - let's add it...
            if (string.IsNullOrEmpty(typeIdentifier))
            {
                var att = (LocalizationTypeIdentifier)type
                    .GetCustomAttribute(typeof(LocalizationTypeIdentifier), inherit: false);

                if (att == null || string.IsNullOrEmpty(att.TypeIdentifier))
                {
                    if (type.BaseType != null && type.AssemblyQualifiedName.StartsWith("System.Data.Entity.DynamicProxies"))
                    {
                        // We are dealing with Entity Framework Proxy
                        typeIdentifier = type.BaseType.Name;
                    }
                    else
                    {
                        typeIdentifier = type.Name;
                    }
                }
                else
                {
                    typeIdentifier = att.TypeIdentifier;
                }

                TypeIdentifiersMap.TryAdd(type, typeIdentifier);
            }

            // Do We have this type mapped>?
            var mapEntry = LocalizablePropertiesMap.ContainsKey(type)
                ? LocalizablePropertiesMap[type] : null;

            // If not - add it...
            if (mapEntry == null)
            {
                var typesLocalizableProperties = type
                    .GetProperties()
                    .Where(pi => pi.GetCustomAttributes(typeof(LocalizableProperty), inherit: true)
                    .Any());

                IDictionary<PropertyInfo, string> dict = new Dictionary<PropertyInfo, string>();

                foreach (var locProp in typesLocalizableProperties)
                {
                    // Get the custom attribute
                    var att = (LocalizableProperty)locProp.GetCustomAttribute(typeof(LocalizableProperty), inherit: true);

                    dict.Add(locProp, string.IsNullOrEmpty(att.FieldIdentifier) ? locProp.Name : att.FieldIdentifier);
                }

                mapEntry = dict;
                LocalizablePropertiesMap.TryAdd(type, mapEntry);
            }
        }
    }
}
