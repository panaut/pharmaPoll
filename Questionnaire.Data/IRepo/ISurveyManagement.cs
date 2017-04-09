using Questionnaire.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data
{
    public interface ISurveyManagement
    {
        IEnumerable<Survey> GetAllSurveys();

        Survey Find(int id);

        Survey CreatePoll(
            string title,
            string json,
            bool isActive = false);

        void UpdateSurvey(
            int id,
            string title,
            string json,
            bool isActive = false);

        void ActivatePoll(int pollId);

        void DeactivatePoll(int pollId);

        void StopPoll(int pollId);

        void DeletePoll(int pollId);
    }
}
