using Questionnaire.Data.Model;
using System.Collections;
using System.Collections.Generic;

namespace Questionnaire.Data
{
    public interface ILocalizationManager
    {
        LocalizedString Find(
            string typeIdentifier,
            int typeUniqueId,
            string fieldIdentifier,
            ECulture culture);

        IEnumerable<LocalizedString> GetLocalizationsForSurvey(int surveyId);

        void UpdateLocalizationsForSurvey(int surveyId, IEnumerable<LocalizedString> localizations);

        void Insert(LocalizedString entry, bool doSave = true);

        void Update(LocalizedString entry, bool doSave = true);

        void DeleteLocalizations(int surveyId);

        void SaveChanges();
    }
}
