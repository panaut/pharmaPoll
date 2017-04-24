using Questionnaire.Data.Model;
using System;
using System.Collections.Generic;

namespace Questionnaire.Data
{
    public class SurveyManager : ISurveyManagement
    {
        private SurveyModelContext context = new SurveyModelContext();

        /// <summary>
        /// Activates survey with given Id.
        /// </summary>
        /// <param name="surveyId">Survey Id.</param>
        /// <exception cref="InvalidOperationException">If survey is already active.</exception>
        public void ActivatePoll(int surveyId)
        {
            var survey = this.Find(surveyId);

            if(survey == null)
            {
                throw new InvalidOperationException($"Survey with Id {surveyId} wasn't found");
            }

            if (survey.IsActive)
                throw new InvalidOperationException($"Survey (SurveyId={surveyId}) is already active");

            survey.IsActive = true;
            context.SaveChanges();
        }

        /// <summary>
        /// Deactivates survey with given Id.
        /// </summary>
        /// <param name="surveyId">Survey Id.</param>
        /// <exception cref="InvalidOperationException">If survey is already inactive.</exception>
        public void DeactivatePoll(int surveyId)
        {
            var survey = this.Find(surveyId);

            if (survey == null)
            {
                throw new InvalidOperationException($"Survey with Id {surveyId} wasn't found");
            }

            if (!survey.IsActive)
                throw new InvalidOperationException($"Survey (SurveyId={surveyId}) is already inactive");

            survey.IsActive = false;
            context.SaveChanges();
        }

        public Survey CreatePoll(string title, string json, bool isActive = false)
        {
            var survey = new Survey();

            survey.title = title;
            survey.SurveyJson = json;
            survey.IsActive = isActive;

            context.Surveys.Add(survey);

            context.SaveChanges();

            return survey;
        }

        public Survey CreatePoll(Survey survey)
        {
            context.Surveys.Add(survey);

            context.SaveChanges();

            return survey;
        }

        public void DeletePoll(int pollId)
        {
            var surveyToDelete = this.Find(pollId);
            context.Surveys.Remove(surveyToDelete);

            context.SaveChanges();
        }

        /// <summary>
        /// Finds the survey with given id.
        /// </summary>
        /// <param name="id">Survey Id</param>
        /// <exception cref="ArgumentOutOfRangeException">If survey with corresponding id isn't found.</exception>
        /// <returns></returns>
        public Survey Find(int id)
        {
            var survey = context.Surveys.Find(id);

            if (survey == null)
                throw new ArgumentOutOfRangeException("surveyId");

            return survey;
        }

        public IEnumerable<Survey> GetAllSurveys()
        {
            return context.Surveys;
        }

        public void StopPoll(int pollId)
        {
            throw new NotImplementedException();
        }

        public void UpdateSurvey(int id, string title, string json, bool isActive = false)
        {
            var survey = this.Find(id);

            if (survey.IsActive)
                throw new InvalidOperationException($"Survey (SurveyId={id}) is already active");

            survey.title = title;
            survey.IsActive = isActive;
            survey.SurveyJson = json;
            context.SaveChanges();
        }
        public void UpdateSurvey(Survey survey)
        {
            var existingSurvey = this.Find(survey.Id);
            var surveyEntry = context.Entry(existingSurvey);
            surveyEntry.CurrentValues.SetValues(survey);

            context.SaveChanges();
        }

    }
}
