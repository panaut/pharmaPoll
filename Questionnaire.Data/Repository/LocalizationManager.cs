using System;
using System.Collections.Generic;
using Questionnaire.Data.Model;
using System.Linq;

namespace Questionnaire.Data
{
    public class LocalizationManager : ILocalizationManager
    {
        private SurveyModelContext context = new SurveyModelContext(true, true, true);

        public LocalizedString Find(
            string typeIdentifier,
            int typeUniqueId,
            string fieldIdentifier,
            ECulture culture)
        {
            var loc = context.LocalizedStrings.Find(typeIdentifier, typeUniqueId, fieldIdentifier, culture);

            return loc;
        }

        public IEnumerable<LocalizedString> GetLocalizationsForSurvey(int surveyId)
        {
            return context.LocalizedStrings.Where(ls => ls.SurveyId == surveyId);
        }

        public void Insert(LocalizedString entry, bool doSave = true)
        {
            context.LocalizedStrings.Add(entry);
            if (doSave)
            {
                context.SaveChanges();
            }
        }

        public void Update(LocalizedString entry, bool doSave = true)
        {
            var dbEntry = context.Entry(entry);

            if (dbEntry.State == System.Data.Entity.EntityState.Detached)
            {
                var dbEntity = this.Find(
                    entry.TypeIdentifier,
                    entry.TypeUniqueId,
                    entry.FieldIdentifier,
                    entry.Culture);

                context.Entry(dbEntity).CurrentValues.SetValues(entry);
            }

            if (doSave)
            {
                context.SaveChanges();
            }
        }

        public void DeleteLocalizations(int surveyId)
        {
            var localizationStringsForSurvey = context.LocalizedStrings.Where(ls => ls.SurveyId == surveyId).ToList();

            foreach (var loc in localizationStringsForSurvey)
            {
                context.LocalizedStrings.Remove(loc);
            }

            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
