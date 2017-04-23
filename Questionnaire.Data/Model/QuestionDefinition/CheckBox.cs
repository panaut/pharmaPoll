using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("checkbox")]
    [Table("SelectBases_CheckBox")]
    public class CheckBox : SelectQuestionBase
    {
        public int colCount { get; set; }

        public bool ShouldSerializecolCount()
        {
            return this.colCount != 0;
        }
    }
}
