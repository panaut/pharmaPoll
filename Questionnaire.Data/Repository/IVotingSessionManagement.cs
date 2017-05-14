using Questionnaire.Data.Model.Results;

namespace Questionnaire.Data
{
    public interface IVotingSessionManagement
    {
        VotingSession Find(int id);

        VotingSession Find(string resumeCode);

        void Insert(VotingSession votingSession);

        void Update(VotingSession votingSession);
    }
}
