using Questionnaire.Data.Model;

namespace Questionnaire.Data
{
    public interface ILocalizationManager
    {
        LocalizedString Find(
            string typeIdentifier,
            int typeUniqueId,
            string fieldIdentifier,
            ECulture culture);

        void Insert(LocalizedString entry, bool doSave = true);

        void Update(LocalizedString entry, bool doSave = true);

        void SaveChanges();
    }
}
