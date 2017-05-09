using Questionnaire.Data.Model.QuestionDefinition;

namespace Questionnaire.Data.Model
{
    public interface IVisitor
    {
        void VisitingNewContainer(ElementContainer container);

        void Visit(ElementBase element);

        void Visit(ElementContainer container);

        void Visit(Page container);

        void Visit(Panel panel);

        void Visit(QuestionBase question);

        void Visit(File question);

        void Visit(Comment question);

        void Visit(Html question);

        void Visit(MultipleText question);

        void Visit(MultipleTextItem question);

        void Visit(Rating question);

        void Visit(Text question);

        void Visit(SelectQuestionBase question);

        void Visit(Choice question);

        void Visit(CheckBox question);

        void Visit(RestfullChoiceSource question);

        void Visit(Dropdown question);

        void Visit(Radiogroup question);

        void Visit(MatrixRow question);

        void Visit(PlainMatrix question);

        void Visit(ComplexMatrixBase question);

        void Visit(ComplexMatrix question);

        void Visit(ComplexMatrixDynamic question);
        
    }
}
