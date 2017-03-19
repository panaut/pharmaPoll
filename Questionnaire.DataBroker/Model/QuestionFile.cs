using Questionnaire.DataBroker.Attributes;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("file")]
    public class QuestionFile : QuestionBaseSelect
    {
        public string imageHeight { get; set; }

        public string imageWidth { get; set; }

        public bool storeDataAsText { get; set; }

        public int maxSize { get; set; }
    }
}
