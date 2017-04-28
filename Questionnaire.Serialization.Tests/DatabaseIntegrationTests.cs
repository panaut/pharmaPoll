using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questionnaire.Service;
using Questionnaire.Data;
using System.Linq;
using Questionnaire.Data.Model.QuestionDefinition;
using Questionnaire.Data.Model;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Questionnaire.Data.Serialization;

namespace Questionnaire.Serialization.Tests
{
    [TestClass]
    public class DatabaseIntegrationTests
    {
        [TestInitialize()]
        public void Init()
        {
            var surveyService = new SurveyService();
            surveyService.CreateOrSaveSurvey(Properties.Resources.complexSurveyJson);
        }

        [TestMethod]
        public void DeleteSurvey()
        {
            var context = new SurveyModelContext();
            var lastSurvey = context.Surveys.OrderByDescending(s => s.Id).First();
            var surveyService = new SurveyService();
            surveyService.DeleteSurvey(lastSurvey.Id);
        }

        [TestMethod]
        public void DeleteQustionsPerType()
        {
            var context = new SurveyModelContext();

            var lastSurvey = context.Surveys.OrderByDescending(s => s.Id).First();

            var pages = context.Elements.OfType<Page>().Where(p => p.SurveyId == lastSurvey.Id);

            foreach (var page in pages)
            {
                // Delete all questions, type per type

                var questions1 = context.Elements.OfType<Text>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions1)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions2 = context.Elements.OfType<CheckBox>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions2)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions3 = context.Elements.OfType<Radiogroup>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions3)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions4 = context.Elements.OfType<Dropdown>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions4)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions5 = context.Elements.OfType<Comment>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions5)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions6 = context.Elements.OfType<Rating>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions6)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions7 = context.Elements.OfType<Html>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions7)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions8 = context.Elements.OfType<File>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions8)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions9 = context.Elements.OfType<PlainMatrix>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions9)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions10 = context.Elements.OfType<ComplexMatrix>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions10)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions11 = context.Elements.OfType<ComplexMatrixDynamic>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions11)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions12 = context.Elements.OfType<MultipleText>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions12)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }

                var questions13 = context.Elements.OfType<Panel>().Where(q => q.ElementContainerId == page.Id);
                foreach (var question in questions13)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }
            }

            context.SaveChanges();
        }

        [TestMethod]
        public void DeserializeSurvey()
        {
            var context = new SurveyModelContext();

            var lastSurvey = context.Surveys.OrderByDescending(s => s.Id).First();

            var surveyJSON = (new SurveySerializer()).Serialize(lastSurvey);
        }

        [TestMethod]
        public void DeleteQustionsPerPage()
        {
            var context = new SurveyModelContext();

            var lastSurvey = context.Surveys.OrderByDescending(s => s.Id).First();

            var pages = context.Elements.OfType<Page>().Where(p => p.SurveyId == lastSurvey.Id);

            foreach (var page in pages)
            {
                // Delete all questions by deletetins page per page

                var questions = page.elements.ToList();
                foreach (var question in questions)
                {
                    context.Entry(question).State = EntityState.Deleted;
                }
            }

            context.SaveChanges();
        }

        [TestMethod]
        public void DeletePages()
        {
            var context = new SurveyModelContext();

            var lastSurvey = context.Surveys.OrderByDescending(s => s.Id).First();

            var pages = context.Elements.OfType<Page>().Where(p => p.SurveyId == lastSurvey.Id).ToList();

            foreach (var page in pages)
            {
                context.Entry(page).State = EntityState.Deleted;
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException uceException)
            {
                var objectContext = ((IObjectContextAdapter)context).ObjectContext;

                //foreach (var objectStateEntry in uceException.Entries)
                //{
                //    objectContext.Refresh(RefreshMode.StoreWins, objectStateEntry.Entity);
                //}

                IObjectContextAdapter adapter = objectContext;

                var deletedEntries = objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted);

                foreach (var deleted in deletedEntries)
                {
                    context.Entry(deleted).State = EntityState.Detached;
                }

                //adapter.ObjectContext.Refresh(
                //    RefreshMode.StoreWins, 
                //    deletedEntries);

                context.SaveChanges();
            }
        }
    }
}
