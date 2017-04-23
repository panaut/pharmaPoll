using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("text")]
    [Table("QDefBases_Text")]
    public class Text : QuestionBase
    {
        public int size { get; set; } = 25;

        public bool ShouldSerializesize()
        {
            return this.size != 25;
        }

        [MaxLength(16)]
        public string inputType { get; set; } = "text";

        public bool ShouldSerializeinputType()
        {
            return this.inputType != "text";
        }

        [MaxLength(512)]
        public string placeHolder { get; set; }

        public bool ShouldSerializeplaceHolder()
        {
            return !string.IsNullOrEmpty(this.placeHolder);
        }
    }
}
