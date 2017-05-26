using Questionnaire.Data;
using Questionnaire.Data.Model;
using Questionnaire.Data.Serialization;
using Questionnaire.Service.Infrastructure;
using System;
using System.Collections.Generic;
using Questionnaire.Service.Extensions;
using System.Linq;
using NLog;

namespace Questionnaire.Service
{
    public class SurveyService : ISurveyService
    {
        private static Logger logger = LogManager.GetLogger("SurveyService");

        private Lazy<ISurveySerializer> surveySerializer;
        private Lazy<ISurveyManagement> surveyManager;
        private Lazy<ILocalizationManager> localizationManager;

        public SurveyService()
        {
            this.surveySerializer = new Lazy<ISurveySerializer>(() => new SurveySerializer());
            this.surveyManager = new Lazy<ISurveyManagement>(() => new SurveyManager());
            this.localizationManager = new Lazy<ILocalizationManager>(() => new LocalizationManager());
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
                                    }
                                }
                                catch (Exception ex)
                                {
                                    cmd.Status = OperationStatus.Failure;
                                    var exc = new CustomException("Failed to save Survey object in database", ex);
                                    throw exc;
                                }

                                // ToDo: IC - Use this code, maybe not here but when requesting localization file...
                                //// Finally - try to extract localizable properties
                                //try
                                //{
                                //    survey.ExtractLocalizations();
                                //}
                                //catch (Exception ex)
                                //{
                                //    cmd.Status = OperationStatus.Failure;
                                //    var exc = new CustomException("Failed to update localizations for survey", ex);
                                //    throw exc;
                                //}

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
                            if (survey.Id == 0)
                            {
                                survey.GenerateUniqueId(4).GenerateTitle();
                                survey = this.surveyManager.Value.CreateSurvey(survey);
                            }
                            else
                            {
                                // This is an update of an existing survey
                                // Find it and check wheter it is active or not
                                var surveyDb = this.surveyManager.Value.Find(survey.Id);

                                // We will update only under condition that it's not active
                                if (!surveyDb.IsActive)
                                    this.surveyManager.Value.UpdateSurvey(survey);
                                else
                                    throw new InvalidOperationException("Active surveys aren't allowed to be updated");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException("Failed to save Survey object in database", ex);
                        throw exc;
                    }

                    // ToDo: IC - Use this code, maybe not here but when requesting localization file...
                    //// Finally - try to extract localizable properties
                    //try
                    //{
                    //    var surveyDb = this.surveyManager.Value.Find(survey.Id);
                    //    surveyDb.ExtractLocalizations();
                    //}
                    //catch (Exception ex)
                    //{
                    //    cmd.Status = OperationStatus.Failure;
                    //    var exc = new CustomException("Failed to update localizations for survey", ex);
                    //    throw exc;
                    //}

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

        public ServiceResponse<string> GetSurvey(int surveyId, string culture = "DEFAULT", bool onlyActive = true)
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
                        var exc = new CustomException(originalException);
                        throw exc;
                    }

                    if (survey != null && onlyActive)
                    {
                        // Check whether the survey is active
                        // If not, raise custom exception
                        if (!survey.IsActive)
                        {
                            cmd.Status = OperationStatus.Failure;
                            var exc = new CustomException(new InvalidOperationException("Requested survey is not active"));
                            throw exc;
                        }
                    }

                    string retStr = string.Empty;      // Result JSON

                    if (survey != null)
                    {
                        // Sort Pages and Elements properly...
                        survey.SortElementsWithinContainer();

                        // Localize Survey
                        ECulture desiredCulture;            // Desired culture enum value
                        if (!Enum.TryParse(culture, out desiredCulture))
                        {
                            logger.Info("Culture {0} is not supported. Survey with ID: {1} will not be localized.", culture, surveyId);
                            desiredCulture = ECulture.DEFAULT;
                        }

                        try
                        {
                            survey.Localize(desiredCulture);
                        }
                        catch (Exception ex)
                        {
                            cmd.Status = OperationStatus.Failure;
                            var exc = new CustomException($"Failed to localize survey with ID: {surveyId} into culture {culture}.", ex);
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
                            var exc = new CustomException("Failed to Serialize survey object into JSON string", ex);
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

        public ServiceResponse<string> GetSurvey(string surveyCode, string culture = "DEFAULT", bool onlyActive = true)
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
                        var exc = new CustomException(originalException);
                        throw exc;
                    }

                    string retStr = string.Empty;      // Result JSON

                    if (survey != null && onlyActive)
                    {
                        // Check whether the survey is active
                        // If not, raise custom exception
                        if (!survey.IsActive)
                        {
                            cmd.Status = OperationStatus.Failure;
                            var exc = new CustomException(new InvalidOperationException("Requested survey is not active"));
                            throw exc;
                        }
                    }

                    if (survey != null)
                    {
                        // Sort Pages and Elements properly...
                        survey.SortElementsWithinContainer();

                        // Localize Survey
                        ECulture desiredCulture;            // Desired culture enum value
                        if (!Enum.TryParse(culture, out desiredCulture))
                        {
                            logger.Info("Culture {0} is not supported. Survey with Code: {1} will not be localized.", culture, surveyCode);
                            desiredCulture = ECulture.DEFAULT;
                        }

                        try
                        {
                            survey.Localize(desiredCulture);
                        }
                        catch (Exception ex)
                        {
                            cmd.Status = OperationStatus.Failure;
                            var exc = new CustomException($"Failed to localize survey with Code: {surveyCode} into culture {culture}.", ex);
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
                            var exc = new CustomException("Failed to Serialize survey object into JSON string", ex);
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

        public ServiceResponse<IEnumerable<Survey>> GetAllSurveys(bool onlyActive = true)
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
                                            .Where(s => !onlyActive || s.IsActive);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException("Failed to retrieve survey objects", ex);
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
                        var exc = new CustomException(originalException);
                        throw exc;
                    }
                }
            };

            command.Execute(surveyId);
            return command.Result;
        }

        public ServiceResponse<byte[]> GetCsvLocalizations(int surveyId, bool doNotExtractAgain = false)
        {
            var command = new ServiceCommand<byte[]>
            {
                Execution = (cmd, parameter) =>
                {
                    if (!doNotExtractAgain)
                    {
                        try
                        {
                            // Get the survey and make sure all Localizable strings are extracted to Localizations
                            var survey = this.surveyManager.Value.Find(surveyId);

                            survey.ExtractLocalizations();
                        }
                        catch (Exception ex)
                        {
                            cmd.Status = OperationStatus.Failure;
                            var exc = new CustomException($"Failed to extract localization strings to CSV for survey with ID: {surveyId}.", ex);
                            throw exc;
                        }
                    }

                    IEnumerable<LocalizedString> localizations = null;
                    byte[] retVal = null;

                    try
                    {
                        localizations = this.localizationManager.Value
                                            .GetLocalizationsForSurvey(surveyId)
                                            .ToList();

                        retVal = localizations.ToCSV();
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException($"Failed to serialize localization strings to CSV for survey with ID: {surveyId}.", ex);
                        throw exc;
                    }

                    cmd.Result.Status = OperationStatus.Success;

                    return retVal;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse UpdateLocalizationFromCSVs(int surveyId, byte[] csvBytes)
        {
            var command = new ServiceCommand
            {
                Execution = (cmd, parameter) =>
                {
                    IEnumerable<LocalizedString> localizedStrings;
                    try
                    {
                        localizedStrings = csvBytes.ParseLocalizationsCSV();
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException($"Failed to parse CSV file for survey with ID: {surveyId}.", ex);
                        throw exc;
                    }

                    if (localizedStrings.Any(ls => ls.SurveyId != surveyId))
                    {
                        cmd.Status = OperationStatus.Failure;
                        var ex = new Exception("Inconsistant CVR data");
                        var exc = new CustomException($"Some of the localizations belong to other survey.", ex);
                        throw exc;
                    }

                    try
                    {
                        foreach (var localization in localizedStrings)
                        {
                            // Get this localization entry if it exists
                            var locStrDb = this.localizationManager.Value.Find(
                                                    localization.TypeIdentifier,
                                                    localization.TypeUniqueId,
                                                    localization.FieldIdentifier,
                                                    localization.Culture);

                            if (locStrDb == null)
                            {
                                // This is a new entry, it should be added to database
                                this.localizationManager.Value.Insert(localization, doSave: false);
                            }
                            else
                            {
                                // This entry already exists, we shall update it
                                this.localizationManager.Value.Update(localization, doSave: false);
                            }
                        }

                        this.localizationManager.Value.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException($"Failed to update localizations for survey with ID: {surveyId}.", ex);
                        throw exc;
                    }

                    cmd.Result.Status = OperationStatus.Success;
                }
            };

            command.Execute(null);
            return command.Result;
        }

        public ServiceResponse DeleteLocalizations(int surveyId)
        {
            var command = new ServiceCommand
            {
                Execution = (cmd, parameter) =>
                {
                    try
                    {
                        this.localizationManager.Value.DeleteLocalizations(surveyId);
                    }
                    catch (Exception ex)
                    {
                        cmd.Status = OperationStatus.Failure;
                        var exc = new CustomException($"Failed to delete localizations for survey with ID: {surveyId}.", ex);
                        throw exc;
                    }

                    cmd.Result.Status = OperationStatus.Success;
                }
            };

            command.Execute(null);
            return command.Result;
        }
    }
}
