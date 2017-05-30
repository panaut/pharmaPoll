using Questionnaire.Service.Infrastructure;
using Questionnaire.Service.Objects;

namespace Questionnaire.Service
{
    public interface IVotingService
    {
        ServiceResponse<string> SaveSession(
            int surveyId,
            string clientIp,
            string results,
            int? lastVisitedPageIndex = null,
            string sessionCode = "",
            bool isFinal = true);

        ServiceResponse<string> SaveSession(
            string surveyCode,
            string clientIp,
            string results,
            int? lastVisitedPageIndex = null,
            string sessionCode = "",
            bool isFinal = true);

        ServiceResponse<VotingSessionInfo> GetSession(
            string surveyCode,
            string sessionCode,
            bool isFinal = false);
    }
}
