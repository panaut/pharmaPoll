using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;
using Questionnaire.Data.Repository.Visitors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data.Repository
{
    public class SurveySynchronizer
    {
        private SurveyModelContext context = null;
        private Survey surveyDb = null;
        public IDictionary<int?, ElementContainer> dbElementContainerDict { get; set; }
        private IEnumerable<QuestionBase> questionsToUpdate;
        private IEnumerable<QuestionBase> questionsToInsert;
        private IEnumerable<ElementContainer> newSurveyContainers;
        private IDictionary<int, ElementContainer> newSurveyContainersDict;

        public SurveySynchronizer(SurveyModelContext context)
        {
            this.context = context;
        }

        public void Syncronize(Survey newSurvey)
        {
            // Get Survey object from Context
            this.surveyDb = context.Surveys.Find(newSurvey.Id);

            // Update Survey object properties
            this.SyncSurvey(newSurvey);

            // Get list of Container Ids, we'll use this later to determine wich ones to delete
            var containerIds = surveyDb.elements.OfType<ElementContainer>().Select(q => q.Id).ToList();

            // Add Element Container objects that don't exist in context but do in newSurvey
            // Additionally, assign a unique UID value for each of them
            this.AddNewContainers(newSurvey);

            // Collect Questions from newSurvey object
            // and set positions of elements within containers
            this.CollectQuestions(newSurvey);

            // Update Questions found both in context and newSurvey
            SyncQuestionsForUpdate(this.questionsToUpdate);

            SyncSurveyTriggers(newSurvey);

            // Delete questions existing in context but not in newSurvey
            var dbIds = surveyDb.elements.OfType<QuestionBase>()
                .Where(q => q.ElementContainerId != null)
                .Select(q => q.Id);
            var idsForDeletion = new HashSet<int>(dbIds.Except(this.questionsToUpdate.Select(q => q.Id)));
            this.DeleteQuestions(idsForDeletion);

            // Perform save so that all containers get their id
            context.SaveChanges();

            // Delete panels that don't exist in new Survey object.
            idsForDeletion = new HashSet<int>(containerIds.Except(this.newSurveyContainers.Select(q => q.Id)));

            // Re-wire questions and containers
            this.RewirePanelsAndQuestions(idsForDeletion);

            this.DeletePanels(idsForDeletion);

            // Finally - save changes
            context.SaveChanges();
        }

        private void SyncSurvey(Survey newSurvey)
        {
            context.Entry(surveyDb).CurrentValues.SetValues(newSurvey);
        }

        private void AddNewContainers(Survey newSurvey)
        {
            var containerNumerator = new ContainerAppender();
            containerNumerator.AppendContainers(context, this.surveyDb, newSurvey);

            this.dbElementContainerDict = containerNumerator.dbElementContainerDict;
        }

        private void CollectQuestions(Survey newSurvey)
        {
            var questionCollector = new QuestionCollector();
            newSurvey.Visit(questionCollector);

            this.questionsToInsert = questionCollector.QuestionsToAdd;
            this.questionsToUpdate = questionCollector.QuestionsToUpdate;

            this.newSurveyContainers = questionCollector.ElementContainers;
            this.newSurveyContainersDict = questionCollector
                .ElementContainers
                .ToDictionary(cont => cont.ContainerUID, cont => cont);
        }

        private void SyncSurveyTriggers(Survey newSurvey)
        {
            // ToDo: IC - Implemet this as soon as triggers get implemented
        }

        private void SyncQuestionsForUpdate(IEnumerable<QuestionBase> qToUpdate)
        {
            var questionUpdater = new QuestionUpdater(this.context);
            foreach (var question in qToUpdate)
            {
                question.Visit(questionUpdater);
            }
        }

        private void DeleteQuestions(HashSet<int> idsForDeletion)
        {
            context.Elements
                .OfType<QuestionBase>()
                .Where(q => idsForDeletion.Contains(q.Id))
                .ToList()
                .ForEach(q => context.Entry(q).State = EntityState.Deleted);
        }

        private void RewirePanelsAndQuestions(HashSet<int> idsOfPanelsToDelete)
        {
            foreach (var panel in this.surveyDb.elements.OfType<Panel>())
            {
                if (!idsOfPanelsToDelete.Contains(panel.Id))
                {
                    var enumeratedPanel = this.newSurveyContainersDict[panel.ContainerUID];
                    var parentContainerDb = this.dbElementContainerDict[panel.ElementContainerUId];
                    panel.PositionWithinContainer = enumeratedPanel.PositionWithinContainer;
                    panel.ElementContainerId = parentContainerDb.Id;
                }
            }

            foreach (var question in this.questionsToUpdate)
            {
                // Get matching question from context
                var questionDb = context.Elements.Find(question.Id);
                var parentContainerDb = this.dbElementContainerDict[question.ElementContainerUId];

                questionDb.ElementContainerId = parentContainerDb.Id;
            }

            var inserter = new QuestionInserter();

            foreach (var question in this.questionsToInsert)
            {
                var parentContainerDb = this.dbElementContainerDict[question.ElementContainerUId];

                parentContainerDb.elements.Add(question);

                question.ElementContainerId = parentContainerDb.Id;
                question.SurveyId = parentContainerDb.SurveyId;

                question.Visit(inserter);
            }
        }

        private void DeletePanels(IEnumerable<int> idsOfContainersToDelete)
        {
            context.Elements
                        .OfType<ElementContainer>()
                        .Where(q => idsOfContainersToDelete.Contains(q.Id))
                        .ToList()
                        .ForEach(q => context.Entry(q).State = EntityState.Deleted);
        }
    }
}
