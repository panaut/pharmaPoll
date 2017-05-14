using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;

namespace Questionnaire.Service.Extensions
{
    internal class VisitorBase : IVisitor
    {
        public virtual void Visit(Survey survey)
        {
        }

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
