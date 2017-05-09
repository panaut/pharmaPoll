using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;

namespace Questionnaire.Data.Model
{
    public interface IVisitable
    {
        void Visit(IVisitor visitor);
    }
}
