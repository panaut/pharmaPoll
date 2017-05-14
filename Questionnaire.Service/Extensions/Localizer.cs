using System;
using System.Collections.Generic;
using System.Linq;
using Questionnaire.Data.Model;
using Questionnaire.Data.Attributes;
using System.Reflection;
using Questionnaire.Data;
using Questionnaire.Data.Model.QuestionDefinition;
using System.Collections.Concurrent;

namespace Questionnaire.Service.Extensions
{
    internal class Localizer : VisitorBase
    {
        private ILocalizationManager locManager;

        private static ConcurrentDictionary<Type, IDictionary<PropertyInfo, string>> LocalizablePropertiesMap
            = new ConcurrentDictionary<Type, IDictionary<PropertyInfo, string>>();

        private static ConcurrentDictionary<Type, string> TypeIdentifiersMap
            = new ConcurrentDictionary<Type, string>();

        public Localizer(ILocalizationManager localizationManager)
        {
            this.locManager = localizationManager;
        }

        private void UpdateMapping(Type type)
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
                    typeIdentifier = type.Name;
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

        private void UpdateLocalizationsForObject(object obj, int id, int surveyId)
        {
            UpdateMapping(obj.GetType());

            // Do we have this type mapped?
            var typeIdentifier = TypeIdentifiersMap[obj.GetType()];

            // Do We have this type mapped>?
            var mapEntry = LocalizablePropertiesMap[obj.GetType()];

            // Now we check whether Localization entries for this type's properties exist
            foreach (var locPropInfo in mapEntry)
            {
                object rawPropValue = locPropInfo.Key.GetValue(obj);
                string currentPropertyValue = rawPropValue != null ? rawPropValue.ToString() : null;

                // Try to get localization entry for this property
                var localizationEntry = this.locManager.Find(
                        typeIdentifier,
                        id,
                        locPropInfo.Value,
                        ECulture.DEFAULT);

                if (localizationEntry == null)
                {
                    // This entry should be inserted
                    this.locManager.Insert(new LocalizedString
                    {
                        TypeIdentifier = typeIdentifier,
                        TypeUniqueId = id,
                        FieldIdentifier = locPropInfo.Value,
                        Culture = ECulture.DEFAULT,
                        SurveyId = surveyId,
                        LocalizedValue = currentPropertyValue
                    },
                    doSave: false);
                }
                else
                {
                    if (localizationEntry.LocalizedValue != currentPropertyValue)
                    {
                        // Since the native value of the string has changed, translations to other languages might not be appropriate

                        // This entry should be updated
                        localizationEntry.LocalizedValue = currentPropertyValue;
                        this.locManager.Update(localizationEntry, doSave: false);
                    }
                }
            }
        }

        public override void Visit(ElementBase element)
        {
            UpdateLocalizationsForObject(element, element.Id, element.SurveyId);
        }

        public override void Visit(Choice choice)
        {
            UpdateLocalizationsForObject(choice, choice.Id, choice.SurveyId);
        }

        public override void Visit(MatrixRow matrixRow)
        {
            UpdateLocalizationsForObject(matrixRow, matrixRow.Id, matrixRow.SurveyId);
        }

        public override void Visit(SelectQuestionBase question)
        {
            foreach (var choice in question.choices)
            {
                choice.Visit(this);
            }
        }

        public override void Visit(ComplexMatrixBase question)
        {
            foreach (var column in question.columns)
            {
                column.Visit(this);
            }
        }

        public override void Visit(ComplexMatrix question)
        {
            foreach (var row in question.rows)
            {
                row.Visit(this);
            }
        }

        public override void Visit(PlainMatrix question)
        {
            foreach (var col in question.columns)
            {
                col.Visit(this);
            }

            foreach (var row in question.rows)
            {
                row.Visit(this);
            }
        }

        public override void Visit(MultipleText question)
        {
            foreach (var item in question.items)
            {
                item.Visit(this);
            }
        }

        public override void Visit(Rating question)
        {
            foreach (var choice in question.choices)
            {
                choice.Visit(this);
            }
        }
    }
}
