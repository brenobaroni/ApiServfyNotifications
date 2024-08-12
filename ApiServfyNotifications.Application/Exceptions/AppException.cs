using System.Net;

namespace ApiServfyNotifications.Application.Exceptions
{
    public class AppException(IList<string> errors, HttpStatusCode? statusCode, Exception? ex = null) : Exception(string.Join(", ", errors))
    {
        public IList<string> Errors { get; set; } = errors;
        public HttpStatusCode? StatusCode { get; set; } = statusCode;
        public Exception? InnerException { get; set; } = ex;
    }
}
