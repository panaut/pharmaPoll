using System;
using Questionnaire.Data.Model;

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

            if(dbEntry.State == System.Data.Entity.EntityState.Detached)
            {
                var dbEntity = this.Find(
                    entry.TypeIdentifier,
                    entry.TypeUniqueId, 
                    entry.FieldIdentifier,
                    entry.Culture);

                context.Entry(dbEntity).CurrentValues.SetValues(entry);
            }

            if(doSave)
            {
                context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
