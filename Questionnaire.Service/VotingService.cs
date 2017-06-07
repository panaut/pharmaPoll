using Questionnaire.Data;
using Questionnaire.Data.Model;
using Questionnaire.Data.Model.Results;
using Questionnaire.Service.Infrastructure;
using System;
using System.Linq;
using Questionnaire.Service.Objects;

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

        public ServiceResponse<string> SaveSession(
            int surveyId,
            string clientIp,
            string results,
            int? lastVisitedPageIndex = default(int?),
            string sessionCode = default(string),
            bool isFinal = true)
        {
            var command = new ServiceCommand<string>
            {
                Execution = (cmd, param) =>
                {
                    Survey survey = null;

                    // Try to get survey with provided Id from database
                    try
                    {
                        survey = this.surveyManager.Value.Find(surveyId);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var originalException = new ArgumentException($"Failed retrieve survey with ID {surveyId}", ex);
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }

                    if (string.IsNullOrEmpty(sessionCode))
                    {
                        // This is the 'new' session, and it's to be inserted
                        sessionCode = this.InitializeSession(
                                                cmd,
                                                survey.surveyId,
                                                clientIp,
                                                results,
                                                lastVisitedPageIndex,
                                                isFinal);
                    }
                    else
                    {
                        // This session is expected to exist - it's to be updated
                        sessionCode = this.UpdateSession(
                                                cmd,
                                                survey.surveyId,
                                                clientIp,
                                                results,
                                                lastVisitedPageIndex,
                                                sessionCode,
                                                isFinal);
                    }

                    return sessionCode;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse<string> SaveSession(
            string surveyCode,
            string clientIp,
            string results,
            int? lastVisitedPageIndex = default(int?),
            string sessionCode = default(string),
            bool isFinal = true)
        {
            var command = new ServiceCommand<string>
            {
                Execution = (cmd, param) =>
                {
                    if (string.IsNullOrEmpty(sessionCode))
                    {
                        // This is the 'new' session, and it's to be inserted
                        sessionCode = this.InitializeSession(
                                                cmd,
                                                surveyCode,
                                                clientIp,
                                                results,
                                                lastVisitedPageIndex,
                                                isFinal);
                    }
                    else
                    {
                        // This session is expected to exist - it's to be updated
                        sessionCode = this.UpdateSession(
                                                cmd,
                                                surveyCode,
                                                clientIp,
                                                results,
                                                lastVisitedPageIndex,
                                                sessionCode,
                                                isFinal);
                    }

                    return sessionCode;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse<VotingSessionInfo> GetSession(
                   string surveyCode,
                   string sessionCode,
                   bool isFinal = false)
        {
            var command = new ServiceCommand<VotingSessionInfo>
            {
                Execution = (cmd, param) =>
                {
                    VotingSession session = null;      // The survey object to be voted

                    // Try to get survey with provided Id from database
                    try
                    {
                        session = this.sessionManager.Value.Find(surveyCode, sessionCode);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var originalException = new ArgumentException($"Failed to retrieve Voting Session: {sessionCode} for survey: {surveyCode}", ex);
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }

                    if (session == null)
                    {
                        var originalException = new InvalidOperationException($"Failed to retrieve Voting Session: {sessionCode} for survey: {surveyCode}");
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }

                    VotingSessionInfo sessionInfo = null;

                    if (!isFinal || !session.IsCompleted)
                    {
                        sessionInfo = new VotingSessionInfo
                        {
                            SessionCode = session.Code,
                            IsComplete = session.IsCompleted,
                            LastPageVisited = session.LastPageVisited.Value,
                            JsonResults = session.JsonData
                        };
                    }

                    cmd.Result.Status = OperationStatus.Success;

                    return sessionInfo;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        private string InitializeSession(
            ServiceCommand<string> cmd,
            string surveyCode,
            string clientIp,
            string results,
            int? lastVisitedPageIndex = null,
            bool isFinal = true)
        {
            Survey survey = null;      // The survey object to be voted

            // Try to get survey with provided Id from database
            try
            {
                survey = this.surveyManager.Value.Find(surveyCode);
            }
            catch (Exception ex)
            {
                cmd.Status = OperationStatus.Failure;
                var originalException = new ArgumentException($"Failed retrieve survey with Code {surveyCode}", ex);
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
                    JsonData = results,
                    LastPageVisited = lastVisitedPageIndex
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

        private string UpdateSession(
            ServiceCommand<string> cmd,
            string surveyCode,
            string clientIp,
            string results,
            int? lastVisitedPageIndex = null,
            string sessionCode = "",
            bool isFinal = true)
        {
            VotingSession session = null;      // The survey object to be voted

            // Try to get survey with provided Id from database
            try
            {
                session = this.sessionManager.Value.Find(surveyCode, sessionCode);
            }
            catch (Exception ex)
            {
                cmd.Status = OperationStatus.Failure;
                var originalException = new ArgumentException($"Failed to retrieve Voting Session: {sessionCode} for survey: {surveyCode}", ex);
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


            // Only incomplete seessions are allowed to be updated
            // If session is inactive (complete), raise custom exception
            if (session.IsCompleted)
            {
                cmd.Status = OperationStatus.Failure;
                var exc = new CustomException(new InvalidOperationException("Requested session is already completed"), errorCode: 0);
                throw exc;
            }

            try
            {
                session.ClientIP = clientIp;
                session.IsCompleted = isFinal;
                session.LastSubmissionTime = DateTime.Now;
                session.NumberOfTimesSubmitted = session.NumberOfTimesSubmitted++;
                session.JsonData = results;
                session.LastPageVisited = lastVisitedPageIndex;

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
    }
}
