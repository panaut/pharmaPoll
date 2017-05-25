using Questionnaire.Data;
using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Extensions
{
    internal class Localizer : LocalizationVisitorBase
    {
        private ECulture targetCulture;

        public Localizer(ECulture culture, ILocalizationManager localizationManager)
            : base(localizationManager)
        {
            this.targetCulture = culture;
        }

        private void LocalizeObject(object obj, int id, int surveyId)
        {
            UpdateMapping(obj.GetType());

            // Do we have this type mapped?
            var typeIdentifier = TypeIdentifiersMap[obj.GetType()];

            // Do We have this type mapped?
            var mapEntry = LocalizablePropertiesMap[obj.GetType()];

            // Now we localize this type's properties
            foreach (var locPropInfo in mapEntry)
            {
                // Let's get localization for this property
                var localizedProperty = this.locManager.Find(typeIdentifier, id, locPropInfo.Value, this.targetCulture);

                if (localizedProperty == null || string.IsNullOrEmpty(localizedProperty.LocalizedValue))
                {
                    // Localization for desired culture doesn't exist
                    // Fallback to default culture
                    localizedProperty = this.locManager.Find(typeIdentifier, id, locPropInfo.Value, ECulture.DEFAULT);
                }

                if (localizedProperty != null && !string.IsNullOrEmpty(localizedProperty.LocalizedValue))
                {
                    // Convert the localized value to match the target property type
                    var converter = TypeDescriptor.GetConverter(locPropInfo.Key.PropertyType); // Converter for type

                    // Convert value
                    var convertedValue = converter.ConvertFrom(localizedProperty);

                    // Now we just have to set the retrieved value
                    locPropInfo.Key.SetValue(obj, convertedValue);
                }
                else
                {
                    // Not even DEFAULT Localization exists - don't localize
                }
            }
        }

        public override void Visit(Survey survey)
        {
            LocalizeObject(survey, survey.Id, survey.Id);
        }

        public override void Visit(ElementBase element)
        {
            LocalizeObject(element, element.Id, element.SurveyId);
        }

        public override void Visit(Choice choice)
        {
            LocalizeObject(choice, choice.Id, choice.SurveyId);
        }

        public override void Visit(MatrixRow matrixRow)
        {
            LocalizeObject(matrixRow, matrixRow.Id, matrixRow.SurveyId);
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
