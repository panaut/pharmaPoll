using Questionnaire.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker
{
    public class SesionManagement : ISessionManagement
    {
        private QuestionnaireModelContainer context;

        private ISessionCodeGenerator codeGen;

        public SesionManagement()
        {
            this.context = new QuestionnaireModelContainer();

            // ToDo: Implement this!
            this.codeGen = null;
        }


        public VotingSession CreateNewSession(int pollId)
        {
            var session = new VotingSession { Date = DateTime.Now, PollId = pollId, Code = this.codeGen.Generate(), State = VotingSessionState.Inactive };

            //this.context.VotingSessions

            return session;
        }

        public void CompleteSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SessionResult> LoadSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public void SaveAnswer(SessionResult answer)
        {
            throw new NotImplementedException();
        }

        public void SaveSession(IEnumerable<SessionResult> answers)
        {
            throw new NotImplementedException();
        }
    }
}
