using Questionnaire.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker
{
    public interface ISectionManagement
    {
        IEnumerable<SurveySection> GetAllSectionsForPoll(int pollId);   

        SurveySection Find(int sectionId);

        SurveySection CreateSection(int pollId, string description, int? displayOrder = null);

        void ChangeDescription(int sectionId, string description);

        void ChangeDisplayOrder(int sectionId, int newValue);

        void DeleteSection(int sectionId, bool forceQuestionDeletion = false);
    }
}
