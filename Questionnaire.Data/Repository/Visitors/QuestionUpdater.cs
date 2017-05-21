using System.Collections.Generic;
using System.Linq;
using Questionnaire.Data.Model.QuestionDefinition;

namespace Questionnaire.Data.Repository.Visitors
{
    internal class QuestionUpdater : VisitorBase
    {
        private SurveyModelContext context = null;

        private QuestionInserter inserter = null;

        public QuestionUpdater(SurveyModelContext context)
        {
            this.context = context;
            this.inserter = new QuestionInserter();
        }

        public override void Visit(QuestionBase question)
        {
            var questionDb = (QuestionBase)context.Elements.Find(question.Id);

            // Make sure parentId is not being overwritten
            var parentId = questionDb.ElementContainerId;
            var surveyId = questionDb.SurveyId;

            // Overrite all values using the new object
            context.Entry(questionDb).CurrentValues.SetValues(question);

            // restore foreign keys (leave question with 'old' parents)
            questionDb.ElementContainerId = parentId;
            questionDb.SurveyId = surveyId;

            // Synchronize Validators
            var idsInDb = questionDb.validators.Select(mti => mti.Id);
            var newIds = question.validators.Select(mti => mti.Id);

            var idsToDelete = new HashSet<int>(idsInDb.Except(newIds));

            if (idsToDelete.Any())
            {
                foreach (var item in questionDb.validators.ToList())
                {
                    if (idsToDelete.Contains(item.Id))
                    {
                        // DELETE
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            // Synchronize contained collection
            foreach (var item in question.validators)
            {
                if (item.Id == 0)
                {
                    // INSERT
                    item.QuestionId = questionDb.Id;
                    context.SurveyValidators.Add(item);
                }
                else
                {
                    // UPDATE
                    var itemDb = questionDb.validators.Single(mti => mti.Id == item.Id);
                    context.Entry(itemDb).CurrentValues.SetValues(item);

                    // Restore Foreign keys
                    itemDb.QuestionId = questionDb.Id;
                }
            }
        }

        public override void Visit(MultipleText question)
        {
            // Sunc collection of multiple text items
            var questionDb = (MultipleText)context.Elements.Find(question.Id);
            var idsInDb = questionDb.items.Select(mti => mti.Id);
            var newIds = question.items.Select(mti => mti.Id);

            var idsToDelete = new HashSet<int>(idsInDb.Except(newIds));

            if(idsToDelete.Any())
            {
                foreach (var item in questionDb.items.ToList())
                {
                    if (idsToDelete.Contains(item.Id))
                    {
                        // DELETE
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            // Synchronize contained collection
            foreach (var item in question.items)
            {
                if (item.Id == 0)
                {
                    // INSERT
                    item.SurveyId = questionDb.SurveyId;
                    item.MultipleTextQuestionId = questionDb.Id;
                    context.Elements.Add(item);
                }
                else
                {
                    // UPDATE
                    var itemDb = questionDb.items.Single(mti => mti.Id == item.Id);
                    context.Entry(itemDb).CurrentValues.SetValues(item);

                    // Restore Foreign keys
                    itemDb.MultipleTextQuestionId = questionDb.Id;
                    itemDb.SurveyId = questionDb.SurveyId;
                }
            }
        }

        public override void Visit(Rating question)
        {
            // Sunc collection of rating choices
            var questionDb = (Rating)context.Elements.Find(question.Id);
            var idsInDb = questionDb.choices.Select(rc => rc.Id);
            var newIds = question.choices.Select(rc => rc.Id);

            var idsToDelete = new HashSet<int>(idsInDb.Except(newIds));

            if (idsToDelete.Any())
            {
                foreach (var item in questionDb.choices.ToList())
                {
                    if (idsToDelete.Contains(item.Id))
                    {
                        // DELETE
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            // Synchronize contained collection
            foreach (var item in question.choices)
            {
                if (item.Id == 0)
                {
                    // INSERT
                    item.SurveyId = questionDb.SurveyId;
                    item.RatingId = questionDb.Id;
                    context.Choices.Add(item);
                }
                else
                {
                    // UPDATE
                    var itemDb = questionDb.choices.Single(ch => ch.Id == item.Id);
                    context.Entry(itemDb).CurrentValues.SetValues(item);
                    itemDb.RatingId = questionDb.Id;
                    itemDb.SurveyId = questionDb.SurveyId;
                }
            }
        }

        public override void Visit(SelectQuestionBase question)
        {
            var questionDb = (SelectQuestionBase)context.Elements.Find(question.Id);

            // Sunc ChoicesByURL object
            if (question.choicesByUrl == null)
            {
                if (questionDb.choicesByUrl != null)
                {
                    context.Entry(questionDb.choicesByUrl).State = System.Data.Entity.EntityState.Deleted;
                }
            }
            else
            {
                if (questionDb.choicesByUrl == null)
                {
                    question.choicesByUrl.Id = questionDb.Id;
                    questionDb.choicesByUrl = question.choicesByUrl;
                }
                else
                {
                    question.choicesByUrl.Id = questionDb.Id;
                    context.Entry(questionDb.choicesByUrl).CurrentValues.SetValues(question.choicesByUrl);
                }
            }

            // Sunc collection of Select Question Choices
            var idsInDb = questionDb.choices.Select(ch => ch.Id);
            var newIds = question.choices.Select(ch => ch.Id);

            var idsToDelete = new HashSet<int>(idsInDb.Except(newIds));

            if(idsToDelete.Any())
            {
                foreach (var item in questionDb.choices.ToList())
                {
                    if (idsToDelete.Contains(item.Id))
                    {
                        // DELETE
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            // Synchronize contained collection
            foreach (var item in question.choices)
            {
                if (item.Id == 0)
                {
                    // INSERT
                    item.SurveyId = questionDb.SurveyId;
                    item.QuestionId = questionDb.Id;
                    context.Choices.Add(item);
                }
                else
                {
                    // UPDATE
                    var itemDb = questionDb.choices.Single(ch => ch.Id == item.Id);
                    context.Entry(itemDb).CurrentValues.SetValues(item);
                    itemDb.QuestionId = questionDb.Id;
                    itemDb.SurveyId = questionDb.SurveyId;
                }
            }
        }

        public override void Visit(ComplexMatrixBase question)
        {
            // Sunc collection of Matrix Columns (sub-questions)
            var questionDb = (ComplexMatrixBase)context.Elements.Find(question.Id);
            var idsInDb = questionDb.columns.Select(col => col.Id);
            var newIds = question.columns.Select(col => col.Id);

            var idsToDelete = new HashSet<int>(idsInDb.Except(newIds));

            if (idsToDelete.Any())
            {
                foreach (var item in questionDb.columns.ToList())
                {
                    if (idsToDelete.Contains(item.Id))
                    {
                        // DELETE
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            var columnsToUpdateDb = questionDb.columns.ToList();

            // Synchronize contained collection
            foreach (var item in question.columns)
            {
                if (item.Id == 0)
                {
                    // INSERT
                    item.SurveyId = questionDb.SurveyId;
                    item.ComplexMatrixBaseId = questionDb.Id;
                    context.Elements.Add(item);

                    item.Visit(inserter);
                }
                else
                {
                    // UPDATE
                    item.Visit(this);
                }
            }

            foreach (var item in columnsToUpdateDb)
            {
                // Set parent (value of foreign key)
                item.ComplexMatrixBaseId = questionDb.Id;
            }
        }

        public override void Visit(ComplexMatrix question)
        {
            // Sunc collection of Matrix Rows
            var questionDb = (ComplexMatrix)context.Elements.Find(question.Id);
            var idsInDb = questionDb.rows.Select(row => row.Id);
            var newIds = question.rows.Select(row => row.Id);

            var idsToDelete = new HashSet<int>(idsInDb.Except(newIds));

            if (idsToDelete.Any())
            {
                foreach (var row in questionDb.columns.ToList())
                {
                    if (idsToDelete.Contains(row.Id))
                    {
                        // DELETE
                        context.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            // Synchronize contained collection
            foreach (var row in question.rows)
            {
                if (row.Id == 0)
                {
                    // INSERT
                    row.SurveyId = questionDb.SurveyId;
                    row.ComplexMatrixId = questionDb.Id;
                    context.MatrixRows.Add(row);
                }
                else
                {
                    // UPDATE
                    var itemDb = questionDb.rows.Single(r => r.Id == row.Id);
                    context.Entry(itemDb).CurrentValues.SetValues(row);
                    itemDb.ComplexMatrixId = questionDb.Id;
                    itemDb.SurveyId = questionDb.SurveyId;
                }
            }
        }

        public override void Visit(PlainMatrix question)
        {
            // Sunc collection of Matrix Rows
            var questionDb = (PlainMatrix)context.Elements.Find(question.Id);

            // Sunc collection of Plain atrix Columns
            var idsOfColumnsInDb = questionDb.columns.Select(col => col.Id);
            var newColumnIds = question.columns.Select(col => col.Id);

            var idsToDelete = new HashSet<int>(idsOfColumnsInDb.Except(newColumnIds));

            if (idsToDelete.Any())
            {
                foreach (var item in questionDb.columns.ToList())
                {
                    if (idsToDelete.Contains(item.Id))
                    {
                        // DELETE
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            // Synchronize contained collection
            foreach (var item in question.columns)
            {
                if (item.Id == 0)
                {
                    // INSERT
                    item.SurveyId = questionDb.SurveyId;
                    item.PlainMatrixId = questionDb.Id;
                    context.Choices.Add(item);
                }
                else
                {
                    // UPDATE
                    var itemDb = questionDb.columns.Single(ch => ch.Id == item.Id);
                    context.Entry(itemDb).CurrentValues.SetValues(item);
                    itemDb.PlainMatrixId = questionDb.Id;
                    itemDb.SurveyId = questionDb.SurveyId;
                }
            }

            var idsOfRowsInDb = questionDb.rows.Select(row => row.Id);
            var newRowIds = question.rows.Select(row => row.Id);

            idsToDelete = new HashSet<int>(idsOfRowsInDb.Except(newRowIds));

            if (idsToDelete.Any())
            {
                foreach (var row in questionDb.rows.ToList())
                {
                    if (idsToDelete.Contains(row.Id))
                    {
                        // DELETE
                        context.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
            }

            // Synchronize contained collection
            foreach (var row in question.rows)
            {
                if (row.Id == 0)
                {
                    // INSERT
                    row.SurveyId = questionDb.SurveyId;
                    row.PlainMatrixId = questionDb.Id;
                    context.MatrixRows.Add(row);
                }
                else
                {
                    // UPDATE
                    var itemDb = questionDb.rows.Single(r => r.Id == row.Id);
                    context.Entry(itemDb).CurrentValues.SetValues(row);
                    itemDb.PlainMatrixId = questionDb.Id;
                    itemDb.SurveyId = questionDb.SurveyId;
                }
            }
        }
    }
}
