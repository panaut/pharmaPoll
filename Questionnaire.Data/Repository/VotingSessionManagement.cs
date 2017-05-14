using System;
using System.Linq;
using Questionnaire.Data.Model.Results;

namespace Questionnaire.Data
{
    public class VotingSessionManagement : IVotingSessionManagement
    {
        private SurveyModelContext context = new SurveyModelContext(true, true, true);

        public VotingSession Find(int id)
        {
            var session = context.VotingSessions.Find(id);

            if (session == null)
                throw new ArgumentOutOfRangeException("SessionId");

            return session;
        }

        public VotingSession Find(string resumeCode)
        {
            var session = context.VotingSessions.Single(s => resumeCode.Equals(s.Code));

            if (session == null)
                throw new ArgumentOutOfRangeException("resumeCode");

            return session;
        }

        public void Insert(VotingSession votingSession)
        {
            context.VotingSessions.Add(votingSession);

            context.SaveChanges();
        }

        public void Update(VotingSession votingSession)
        {
            var dbEntry = context.Entry(votingSession);

            if (dbEntry.State == System.Data.Entity.EntityState.Detached)
            {
                var dbEntity = this.Find(votingSession.Id);

                context.Entry(dbEntity).CurrentValues.SetValues(votingSession);
            }

            context.SaveChanges();
        }
    }
}
