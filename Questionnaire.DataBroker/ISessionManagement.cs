using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker
{
    public interface ISessionManagement
    {
        VotingSession CreateNewSession(int pollId);

        IEnumerable<SessionResult> LoadSession(string sessionId);

        void SaveSession(IEnumerable<SessionResult> answers);

        void CompleteSession(string sessionId);

        void SaveAnswer(SessionResult answer);
        
    }
}
