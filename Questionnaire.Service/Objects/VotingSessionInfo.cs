namespace Questionnaire.Service.Objects
{
    public class VotingSessionInfo
    {
        public bool IsComplete { get; set; }

        public string SessionCode { get; set; }

        public int LastPageVisited { get; set; }

        public string JsonResults { get; set; }
    }
}
