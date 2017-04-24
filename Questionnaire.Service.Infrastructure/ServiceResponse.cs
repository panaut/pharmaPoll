namespace Questionnaire.Service.Infrastructure
{
    public class ServiceResponse
    {
        public OperationStatus Status { get; set; }

        public int? ErrorCode { get; set; }
    }
}
