using Questionnaire.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker
{
    public interface IPollManagement
    {
        IEnumerable<Poll> GetAllPolls();

        Poll Find(int id);

        Poll CreatePoll(
            string name,
            string desc = null,
            bool isActive = false,
            DateTime? start = null,
            TimeSpan? duration = null);

        void SetHeader(int pollId, string header);

        void SetFooter(int pollId, string footer);

        void ActivatePoll(int pollId);

        void StopPoll(int pollId);

        void DeletePoll(int pollId);
    }
}
