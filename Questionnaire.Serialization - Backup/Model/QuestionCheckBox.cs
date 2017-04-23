using Questionnaire.Serialization.Attributes;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("checkbox")]
    public class QuestionCheckBox : QuestionBaseSelect
    {
        public int colCount { get; set; }

        public bool ShouldSerializecolCount()
        {
            return this.colCount != 0;
        }
    }
}
