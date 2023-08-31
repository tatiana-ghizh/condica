namespace CVU.CONDICA.ExceptionHandling.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {

        }

        public ErrorResponse(FailureReason failureReason)
        {
            FailureReason = failureReason;
        }

        public ErrorResponse(FailureReason failureReason, IEnumerable<string> messages)
        {
            FailureReason = failureReason;
            Messages = messages;
        }

        public ErrorResponse(FailureReason failureReason, IEnumerable<string> messages, string details)
        {
            FailureReason = failureReason;
            Messages = messages;
            Detail = details;
        }

        public FailureReason FailureReason { get; set; }
        public IEnumerable<string> Messages { get; set; }
        public string Detail { get; set; }
    }
}
