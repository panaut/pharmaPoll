namespace Questionnaire.DataBroker.Model
{
    public class QuestionFileBase : QuestionBaseSelect
    {
        public string imageHeight { get; set; }
        public string imageWidth { get; set; }
        public bool storeDataAsText { get; set; }
        public int maxSize { get; set; }
    }
}
