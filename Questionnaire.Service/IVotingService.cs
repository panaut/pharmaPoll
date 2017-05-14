using Questionnaire.Service.Infrastructure;

namespace Questionnaire.Service
{
    public interface IVotingService
    {
        ServiceResponse<string> InitializeSession(
            int surveyId,
            string clientIp, 
            string results,
            bool isFinal = true);

        ServiceResponse<string> UpdateSession(
            string sessionCode,
            string results,
            bool isFinal = true);
    }
}
