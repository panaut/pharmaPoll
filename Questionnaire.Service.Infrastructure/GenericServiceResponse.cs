namespace Questionnaire.Service.Infrastructure
{
    public class ServiceResponse<T> : ServiceResponse
    {
        public T OperationResult { get; set; }
    }
}
