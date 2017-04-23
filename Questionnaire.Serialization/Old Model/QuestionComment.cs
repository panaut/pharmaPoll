using Questionnaire.Serialization.Attributes;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("comment")]
    public class QuestionComment : QuestionBaseSelect
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

        public string placeHolder { get; set; }

        public bool ShouldSerializeplaceHolder()
        {
            return !string.IsNullOrEmpty(this.placeHolder);
        }
    }
}
