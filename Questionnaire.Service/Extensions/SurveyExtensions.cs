using Questionnaire.Data;
using Questionnaire.Data.Model;
using System;
using System.Linq;

namespace Questionnaire.Service.Extensions
{
    public static class SurveyExtensions
    {
        public static Survey GenerateUniqueId(this Survey survey, int lenght)
        {
            if (survey.surveyId == null)
            {
                survey.surveyId = string.Join("", Guid.NewGuid().ToString("n").Take(lenght).Select(o => o)).ToUpper();
            }

            return survey;
        }
        public static Survey GenerateTitle(this Survey survey)
        {
            if (string.IsNullOrEmpty(survey.title))
            {
                survey.title = $"pqSurvey {DateTime.Now.ToString("yy-MM-dd HH:mm:ss")}";
            }

            return survey;
        }

        public static Survey SortElementsWithinContainer(this Survey survey)
        {
            // Sort pages
            survey.elements = survey.elements.OfType<Page>().OrderBy(el => el.PositionWithinContainer).ToArray();

            var sorter = new ElementSorter();
            survey.Visit(sorter);

            return survey;
        }

        public static Survey ExtractLocalizations(this Survey survey)
        {
            var localizationManager = new LocalizationManager();
            var extractor = new LocalizationExtractor(localizationManager);

            survey.Visit(extractor);

            localizationManager.SaveChanges();

            return survey;
        }

        public static Survey Localize(this Survey survey, ECulture culture)
        {
            var localizationManager = new LocalizationManager();
            var localizer = new Localizer(culture, localizationManager);

            survey.Visit(localizer);

            return survey;
        }

        private class ElementSorter : VisitorBase
        {
            public override void Visit(ElementContainer container)
            {
                container.elements = container.elements.OrderBy(el => el.PositionWithinContainer).ToArray();
            }
        }
    }
}
