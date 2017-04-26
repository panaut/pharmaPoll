using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("comment")]
    [Table("QuestionBases_Comment")]
    public class Comment : QuestionBase
    {
        public int rows { get; set; } = 4;

        public bool ShouldSerializerows()
        {
            return this.rows != 4;
        }

        public int cols { get; set; } = 50;

        public bool ShouldSerializecols()
        {
            return this.cols != 50;
        }

        [MaxLength(256)]
        public string placeHolder { get; set; }

        public bool ShouldSerializeplaceHolder()
        {
            return !string.IsNullOrEmpty(this.placeHolder);
        }
    }
}
