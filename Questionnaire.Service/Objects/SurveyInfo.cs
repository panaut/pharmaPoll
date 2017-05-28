namespace Questionnaire.Service.Objects
{
    public class SurveyInfo
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public int CompletedSessionCount { get; set; }

        public int TotalSessionCount { get; set; }

        public SurveyInfo(
                    int id, 
                    string code,
                    string title, 
                    bool isActive, 
                    int completedSessionsCount,
                    int totalSessionCount)
        {
            this.Id = id;
            this.Code = code;
            this.Title = title;
            this.IsActive = isActive;
            this.CompletedSessionCount = completedSessionsCount;
            this.TotalSessionCount = totalSessionCount;
        }
    }
}
