using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questionnaire.Service;
using Questionnaire.Data;
using System.Linq;
using Questionnaire.Data.Model.QuestionDefinition;

namespace Questionnaire.Serialization.Tests
{
    [TestClass]
    public class DatabaseIntegrationTests
    {
        [TestInitialize()]
        public void Init()
        {
            var surveyService = new SurveyService();
            surveyService.CreateOrSaveSurvey(Properties.Resources.surveyJson);
        }

        [TestMethod]
        public void DeleteSurvey()
        {

            var surveyService = new SurveyService();
            surveyService.DeleteSurvey(1);
        }

        [TestMethod]
        public void DeleteQustions()
        {
            var context = new SurveyModelContext();

            var questions1 = context.Questions.OfType<Text>();
            foreach (var question in questions1)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions2 = context.Questions.OfType<CheckBox>();
            foreach (var question in questions2)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions3 = context.Questions.OfType<Radiogroup>();
            foreach (var question in questions3)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions4 = context.Questions.OfType<Dropdown>();
            foreach (var question in questions4)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions5 = context.Questions.OfType<Comment>();
            foreach (var question in questions5)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions6 = context.Questions.OfType<Rating>();
            foreach (var question in questions6)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions7 = context.Questions.OfType<Html>();
            foreach (var question in questions7)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions8 = context.Questions.OfType<File>();
            foreach (var question in questions8)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions9 = context.Questions.OfType<PlainMatrix>();
            foreach (var question in questions9)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions10 = context.Questions.OfType<ComplexMatrix>();
            foreach (var question in questions10)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions11 = context.Questions.OfType<ComplexMatrixDynamic>();
            foreach (var question in questions11)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            var questions12 = context.Questions.OfType<MultipleText>();
            foreach (var question in questions12)
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
            }

            context.SaveChanges();
        }
    }
}
