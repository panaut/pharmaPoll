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
                                        surveyInDb = this.surveyManager.Value.CreatePoll(survey.title, surveyJson, isActive: true);


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

                    Survey surveyInDb = null;   // Database record representing the survey

                    // Try to save deserialized object to database
                    try
                    {
                        if (survey != null)
                        {
                            if (string.IsNullOrEmpty(survey.surveyId))
                            {
                                surveyInDb = this.surveyManager.Value.CreatePoll(survey.title, surveyJson, isActive: true);
                            }
                            else
                            {
                                int surveyId = 0;

                                if (!int.TryParse(survey.surveyId, out surveyId))
                                {
                                    throw new CustomException(new ArgumentException($"Failed to parse SurveyId (value was: {survey.surveyId})", "surveyId"));
                                }

                                this.surveyManager.Value.UpdateSurvey(surveyId, survey.title, surveyJson, isActive: true);
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

                    return surveyInDb.Id;
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
                        surveyInDb = this.surveyManager.Value.GetAllSurveys().First(srv => srv.IsActive);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var originalException = new ArgumentException("Failed to save Survey object in database", ex);
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }

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

                    string retStr;      // Result JSON

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

                    return retStr;
                }
            };

            command.Execute(null);
            return command.Result;
        }
    }
}
