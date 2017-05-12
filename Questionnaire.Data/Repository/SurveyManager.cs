using Questionnaire.Data.Model;
using System;
using System.Collections.Generic;
using RefactorThis.GraphDiff;
using System.Linq;
using Questionnaire.Data.Model.QuestionDefinition;
using Questionnaire.Data.Repository;
using Questionnaire.Data.Repository.Visitors;

namespace Questionnaire.Data
{
    public class SurveyManager : ISurveyManagement
    {
        private SurveyModelContext context = new SurveyModelContext(true, true, true);

        /// <summary>
        /// Activates survey with given Id.
        /// </summary>
        /// <param name="surveyId">Survey Id.</param>
        /// <exception cref="InvalidOperationException">If survey is already active.</exception>
        public void ActivatePoll(int surveyId)
        {
            var survey = this.Find(surveyId);

            if (survey == null)
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

        public Survey CreateSurvey(Survey survey)
        {
            QuestionCollector qCollector = new QuestionCollector();

            // Write indexes to pages
            var pageIndex = 1;
            foreach(var page in survey.elements.OfType<Page>())
            {
                page.PositionWithinContainer = pageIndex++;
            }

            // Write indexes to elements within containers
            survey.Visit(qCollector);

            context.Surveys.Add(survey);

            context.SaveChanges();

            return survey;
        }

        public void DeleteSurvey(int pollId)
        {
            var surveyToDelete = this.Find(pollId);
            context.Surveys.Remove(surveyToDelete);

            context.SaveChanges();
        }

        /// <summary>
        /// Finds the survey with given int id.
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

        /// <summary>
        /// Finds the survey with given string id.
        /// </summary>
        /// <param name="code">Survey Id</param>
        /// <exception cref="ArgumentOutOfRangeException">If survey with corresponding id isn't found.</exception>
        /// <returns></returns>
        public Survey Find(string code)
        {
            var survey = context.Surveys.SingleOrDefault(s => s.surveyId.Equals(code));

            if (survey == null)
                throw new ArgumentOutOfRangeException("code");

            return survey;
        }

        public IEnumerable<Survey> GetAllSurveys()
        {
            return context.Surveys;
        }

        public void UpdateSurvey(Survey survey)
        {
            var synchronizer = new SurveySynchronizer(context);
            synchronizer.Syncronize(survey);
        }
    }
}
