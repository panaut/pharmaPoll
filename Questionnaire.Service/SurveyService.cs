using Questionnaire.Data;
using Questionnaire.Serialization;
using Questionnaire.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service
{
    public class SurveyService : ISurveyService
    {
        private Lazy<ISurveySerializer> surveySerializer;

        private Lazy<ISurveyManagement> surveyManager;

        public SurveyService()
        {
            this.surveySerializer = new Lazy<ISurveySerializer>(() => new SurveySerializer());
            this.surveyManager = new Lazy<ISurveyManagement>(() => new SurveyManager());
        }

        public ServiceResponse<int> CreateSurvey(string surveyJson)
        {
            var command = new ServiceCommand<int>
            {
                Execution = (cmd, param) =>
                            {
                                Serialization.Model.Survey survey = null;      // The survey object to be saved

                                // Try to deserialize JSON string
                                try
                                {
                                    survey = surveySerializer.Value.Deserialize(surveyJson);
                                }
                                catch (Exception ex)
                                {
                                    cmd.Status = OperationStatus.Failure;
                                    var exc = new CustomException("Failed to Deserialize survey object", ex);
                                    throw exc;
                                }

                                Survey surveyInDb = null;   // Database record representing the survey

                                // Try to save deserialized object to database
                                try
                                {
                                    if (survey != null)
                                    {
                                        if (string.IsNullOrEmpty(survey.surveyId))
                                        {
                                            survey.surveyId = $"pqSurvey-{DateTime.Now.ToString("yyMMddHHmmss")}";
                                        }

                                        surveyInDb = this.surveyManager.Value.CreatePoll(survey.surveyId, survey.title, surveyJson, isActive: true);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    cmd.Status = OperationStatus.Failure;
                                    var exc = new CustomException("Failed to save Survey object in database", ex);
                                    throw exc;
                                }

                                cmd.Result.Status = OperationStatus.Success;

                                return surveyInDb.Id;
                            }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse<int> CreateOrSaveSurvey(string surveyJson)
        {
            ServiceCommand<int> command = new ServiceCommand<int>
            {
                Execution = (cmd, param) =>
                {
                    Serialization.Model.Survey survey = null;      // The survey object to be saved

                    // Try to deserialize JSON string
                    try
                    {
                        survey = surveySerializer.Value.Deserialize(surveyJson);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException("Failed to Deserialize survey object", ex);
                        throw exc;
                    }

                    int surveyId = 0;   // Id of the Database record representing the survey

                    // Try to save deserialized object to database
                    try
                    {
                        if (survey != null)
                        {
                            if (string.IsNullOrEmpty(survey.surveyId))
                            {
                                var surveyInDb = this.surveyManager.Value.CreatePoll(survey.surveyId, survey.title, surveyJson, isActive: false);
                                surveyId = surveyInDb.Id;
                            }
                            else
                            {
                                if (!int.TryParse(survey.surveyId, out surveyId))
                                {
                                    throw new CustomException(new ArgumentException($"Failed to parse SurveyId (value was: {survey.surveyId})", "surveyId"));
                                }

                                this.surveyManager.Value.UpdateSurvey(surveyId, survey.surveyId, survey.title, surveyJson, isActive: false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException("Failed to save Survey object in database", ex);
                        throw exc;
                    }

                    cmd.Result.Status = OperationStatus.Success;

                    return surveyId;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse<string> GetActiveSurvey()
        {
            var command = new ServiceCommand<string>
            {
                Execution = (cmd, param) =>
                {
                    Survey surveyInDb = null;      // The survey object to be saved

                    // Try to get active survey from database
                    try
                    {
                        surveyInDb = this.surveyManager.Value.GetAllSurveys().SingleOrDefault(srv => srv.IsActive);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var originalException = new ArgumentException("Failed retrieve active survey", ex);
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }

                    string retStr = string.Empty;      // Result JSON

                    if (surveyInDb != null)
                    {
                        var surveyJson = surveyInDb.SurveyJson;
                        Serialization.Model.Survey survey = null;
                        // Try to deserialize JSON string from database
                        try
                        {
                            survey = surveySerializer.Value.Deserialize(surveyJson);
                            survey.surveyId = surveyInDb.Id.ToString();
                        }
                        catch (Exception ex)
                        {
                            cmd.Status = OperationStatus.Failure;
                            var exc = new CustomException("Failed to Deserialize survey object", ex, errorCode: 0);
                            throw exc;
                        }

                        // Try to serialize modified object into JSON string
                        try
                        {
                            retStr = surveySerializer.Value.Serialize(survey);
                        }
                        catch (Exception ex)
                        {
                            cmd.Status = OperationStatus.Failure;
                            var exc = new CustomException("Failed to Serialize survey object into JSON string", ex, errorCode: 0);
                            throw exc;
                        }

                        cmd.Result.Status = OperationStatus.Success;
                    }

                    return retStr;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse<IEnumerable<Survey>> GetAllSurveys()
        {
            var command = new ServiceCommand<IEnumerable<Survey>>
            {
                Execution = (cmd, parameter) =>
                {
                    IEnumerable<Survey> surveys = null;

                    try
                    {
                        surveys = this.surveyManager.Value
                                            .GetAllSurveys()
                                            .Select(srv => { srv.SurveyJson = string.Empty; return srv; });
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException("Failed to retrieve survey objects", ex, errorCode: 0);
                        throw exc;
                    }

                    return surveys;
                }
            };

            command.Execute(null);
            return command.Result;
        }
    }
}
