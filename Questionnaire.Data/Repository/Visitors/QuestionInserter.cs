using Questionnaire.Data.Model.QuestionDefinition;

namespace Questionnaire.Data.Repository.Visitors
{
    internal class QuestionInserter : VisitorBase
    {
        public override void Visit(SelectQuestionBase question)
        {
            foreach (var choice in question.choices)
            {
                choice.SurveyId = question.SurveyId;
            }
        }

        public override void Visit(MultipleText question)
        {
            foreach (var item in question.items)
            {
                item.SurveyId = question.SurveyId;
            }
        }

        public override void Visit(PlainMatrix question)
        {
            foreach (var column in question.columns)
            {
                column.SurveyId = question.SurveyId;
            }

            foreach (var row in question.rows)
            {
                row.SurveyId = question.SurveyId;
            }
        }

        public override void Visit(ComplexMatrix question)
        {
            foreach (var row in question.rows)
            {
                row.SurveyId = question.SurveyId;
            }
        }

        public override void Visit(ComplexMatrixBase question)
        {
            foreach (var column in question.columns)
            {
                column.SurveyId = question.SurveyId;
                column.Visit(this);
            }
        }

        public override void Visit(Rating question)
        {
            foreach (var choice in question.choices)
            {
                choice.SurveyId = question.SurveyId;
            }
        }
    }
}
