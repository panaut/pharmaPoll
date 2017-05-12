using Questionnaire.Data;
using Questionnaire.Data.Model;
using Questionnaire.Data.Serialization;
using Questionnaire.Service.Infrastructure;
using System;
using System.Collections.Generic;
using Questionnaire.Service.Extensions;

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
                                Survey survey = null;      // The survey object to be saved

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

                                // Try to save deserialized object to database
                                try
                                {
                                    if (survey != null)
                                    {
                                        survey.GenerateUniqueId(4).GenerateTitle();
                                        survey = this.surveyManager.Value.CreateSurvey(survey);

                                        //if (survey.Id == 0)
                                        //{

                                        //}
                                        //else
                                        //{
                                        //    throw new CustomException(
                                        //        "Failed to save Survey object in database",
                                        //        new ArgumentException("It's not allowed to insert Surveys with already assigned ID", "Survey.Id"));
                                        //}

                                    }
                                }
                                catch (Exception ex)
                                {
                                    cmd.Status = OperationStatus.Failure;
                                    var exc = new CustomException("Failed to save Survey object in database", ex);
                                    throw exc;
                                }

                                cmd.Result.Status = OperationStatus.Success;

                                return survey.Id;
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
                    Survey survey = null;      // The survey object to be saved

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

                    // Try to save deserialized object to database
                    try
                    {
                        if (survey != null)
                        {
                            survey.GenerateUniqueId(4).GenerateTitle();

                            if (survey.Id == 0)
                            {
                                survey = this.surveyManager.Value.CreateSurvey(survey);
                            }
                            else
                            {
                                this.surveyManager.Value.UpdateSurvey(survey);
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

                    return survey.Id;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse<bool?> SetSurveyStatus(int surveyId, bool status)
        {
            ServiceCommand<bool?> command = new ServiceCommand<bool?>
            {
                Execution = (cmd, param) =>
                {
                    bool? surveyStatus = null;

                    try
                    {
                        int id = (int)param;

                        if (status)
                        {
                            // Activate Survey
                            this.surveyManager.Value.ActivatePoll(id);
                            surveyStatus = true;
                        }
                        else
                        {
                            // Deactivate Survey
                            this.surveyManager.Value.DeactivatePoll(id);
                            surveyStatus = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException("Failed to update Survey status", ex);
                        throw exc;
                    }

                    cmd.Result.Status = OperationStatus.Success;

                    return surveyStatus;
                }
            };

            command.Execute(surveyId);
            return command.Result;
        }

        public ServiceResponse<string> GetSurvey(int surveyId)
        {
            var command = new ServiceCommand<string>
            {
                Execution = (cmd, param) =>
                {
                    Survey survey = null;      // The survey object to be saved

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

                    string retStr = string.Empty;      // Result JSON

                    if (survey != null)
                    {
                        // Sort Pages and Elements properly...
                        survey.SortElementsWithinContainer();

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

        public ServiceResponse<string> GetSurvey(string surveyCode)
        {
            var command = new ServiceCommand<string>
            {
                Execution = (cmd, param) =>
                {
                    Survey survey = null; // Try to get survey with provided Code from database

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

                    string retStr = string.Empty;      // Result JSON

                    if (survey != null)
                    {
                        // Sort Pages and Elements properly...
                        survey.SortElementsWithinContainer();

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
                                            .GetAllSurveys();
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

        public ServiceResponse DeleteSurvey(int surveyId)
        {
            ServiceCommand command = new ServiceCommand
            {
                Execution = (cmd, param) =>
                {
                    // Try to get survey with provided Id from database
                    try
                    {
                        this.surveyManager.Value.DeleteSurvey(surveyId);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var originalException = new ArgumentException($"Failed delete survey with id {surveyId}", ex);
                        var exc = new CustomException(originalException, errorCode: 0);
                        throw exc;
                    }
                }
            };

            command.Execute(surveyId);
            return command.Result;
        }
    }
}
