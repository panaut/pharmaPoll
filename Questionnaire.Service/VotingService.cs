using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Questionnaire.Service.Infrastructure;
using Questionnaire.Data;
using Questionnaire.Data.Repository;
using Questionnaire.Data.Model;
using Questionnaire.Data.Model.Results;

namespace Questionnaire.Service
{
    public class VotingService : IVotingService
    {
        private Lazy<ISurveyManagement> surveyManager;
        private Lazy<IVotingSessionManagement> sessionManager;

        public VotingService()
        {
            this.surveyManager = new Lazy<ISurveyManagement>(() => new SurveyManager());
            this.sessionManager = new Lazy<IVotingSessionManagement>(() => new VotingSessionManagement());
        }

        public ServiceResponse<string> InitializeSession(int surveyId, string clientIp, string results, bool isFinal = true)
        {
            var command = new ServiceCommand<string>
            {
                Execution = (cmd, param) =>
                {
                    Survey survey = null;      // The survey object to be voted

                    // Try to get survey with provided Id from database
                    try
                    {
                        survey = this.surveyManager.Value.Find(surveyId);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var originalException = new ArgumentException($"Failed retrieve survey with id {surveyId}", ex);
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }

                    // Only active surveys are permitted to be voted, so let's check whether the survey is active
                    // If not, raise custom exception
                    if (!survey.IsActive)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException(new InvalidOperationException("Requested survey is not active"), errorCode: 0);
                        throw exc;
                    }

                    // Generate Very-Unlikely-To-Be-Repeated 'Random' Code :-)
                    var sessionCode = string.Join("", Guid.NewGuid().ToString("n").Take(4).Select(o => o)).ToUpper();

                    try
                    {
                        VotingSession session = new VotingSession
                        {
                            SurveyId = survey.Id,
                            ClientIP = clientIp,
                            IsCompleted = isFinal,
                            Code = sessionCode,
                            InitializationTime = DateTime.Now,
                            LastSubmissionTime = DateTime.Now,
                            NumberOfTimesSubmitted = 0,
                            JsonData = results
                        };

                        sessionManager.Value.Insert(session);
                    }
                    catch (Exception)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException(new Exception("Voting Session could not be saved"), errorCode: 0);
                        throw exc;
                    }

                    cmd.Result.Status = OperationStatus.Success;

                    return sessionCode;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse<string> UpdateSession(string sessionCode, string results, bool isFinal = true)
        {
            var command = new ServiceCommand<string>
            {
                Execution = (cmd, param) =>
                {
                    VotingSession session = null;      // The survey object to be voted

                    // Try to get survey with provided Id from database
                    try
                    {
                        session = this.sessionManager.Value.Find(sessionCode);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var originalException = new ArgumentException($"Failed retrieve Voting Session with Code {sessionCode}", ex);
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }

                    // Only active surveys are permitted to be voted, so let's check whether the survey is active
                    // If not, raise custom exception
                    if (!session.Survey.IsActive)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException(new InvalidOperationException("Requested survey is not active"), errorCode: 0);
                        throw exc;
                    }

                    try
                    {

                        session.LastSubmissionTime = DateTime.Now;
                        session.NumberOfTimesSubmitted++;
                        session.IsCompleted = isFinal;
                        session.JsonData = results;

                        sessionManager.Value.Update(session);
                    }
                    catch (Exception)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException(new Exception("Voting Session could not be updated"), errorCode: 0);
                        throw exc;
                    }

                    cmd.Result.Status = OperationStatus.Success;

                    return sessionCode;
                }
            };

            command.Execute(null);
            return command.Result;
        }
    }
}
