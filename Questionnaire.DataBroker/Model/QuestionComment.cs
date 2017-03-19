using Questionnaire.DataBroker.Attributes;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("comment")]
    public class QuestionComment : QuestionBaseSelect
    {
        public int rows { get; set; } = 4;

        public int cols { get; set; } = 50;

        public string placeHolder { get; set; }
    }
}
