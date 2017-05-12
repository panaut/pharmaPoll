using Questionnaire.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Questionnaire.Data.Model.QuestionDefinition;

namespace Questionnaire.Service.Extensions
{
    public static class SurveyExtensions
    {
        public static Survey GenerateUniqueId(this Survey survey, int lenght)
        {
            if (survey.surveyId == null)
            {
                survey.surveyId = string.Join("", Guid.NewGuid().ToString("n").Take(lenght).Select(o => o)).ToUpper();
            }

            return survey;
        }
        public static Survey GenerateTitle(this Survey survey)
        {
            if (string.IsNullOrEmpty(survey.title))
            {
                survey.title = $"pqSurvey {DateTime.Now.ToString("yy-MM-dd HH:mm:ss")}";
            }

            return survey;
        }

        public static Survey SortElementsWithinContainer(this Survey survey)
        {
            // Sort pages
            survey.elements = survey.elements.OfType<Page>().OrderBy(el => el.PositionWithinContainer).ToArray();

            var sorter = new ElementSorter();
            survey.Visit(sorter);

            return survey;
        }

        private class ElementSorter : VisitorBase
        {
            public override void Visit(ElementContainer container)
            {
                container.elements = container.elements.OrderBy(el => el.PositionWithinContainer).ToArray();
            }
        }

        private class VisitorBase : IVisitor
        {
            public virtual void Visit(ElementBase element)
            {
            }

            public virtual void Visit(Panel panel)
            {
            }

            public virtual void Visit(File question)
            {
            }

            public virtual void Visit(Html question)
            {
            }

            public virtual void Visit(MultipleTextItem question)
            {
            }

            public virtual void Visit(Text question)
            {
            }

            public virtual void Visit(Choice question)
            {
            }

            public virtual void Visit(RestfullChoiceSource question)
            {
            }

            public virtual void Visit(Radiogroup question)
            {
            }

            public virtual void Visit(PlainMatrix question)
            {
            }

            public virtual void Visit(ComplexMatrix question)
            {
            }

            public virtual void Visit(ComplexMatrixDynamic question)
            {
            }

            public virtual void Visit(ComplexMatrixBase question)
            {
            }

            public virtual void Visit(MatrixRow question)
            {
            }

            public virtual void Visit(Dropdown question)
            {
            }

            public virtual void Visit(CheckBox question)
            {
            }

            public virtual void Visit(SelectQuestionBase question)
            {
            }

            public virtual void Visit(Rating question)
            {
            }

            public virtual void Visit(MultipleText question)
            {
            }

            public virtual void Visit(Comment question)
            {
            }

            public virtual void Visit(QuestionBase question)
            {
            }

            public virtual void Visit(Page container)
            {
            }

            public virtual void Visit(ElementContainer container)
            {
            }

            public virtual void VisitingNewContainer(ElementContainer container)
            {
            }
        }
    }
}
