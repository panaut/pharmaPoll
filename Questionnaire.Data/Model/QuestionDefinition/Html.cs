using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("html")]
    [Table("QuestionBases_Html")]
    public class Html : QuestionBase
    {
        public string html { get; set; }

        public bool ShouldSerializehtml()
        {
            return !string.IsNullOrEmpty(this.html);
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
