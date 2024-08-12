using System.Net;
using System.Reflection;

namespace ApiServfyNotifications.Application.Exceptions
{
    public class AppCrossCuttingException(IList<string> errors, HttpStatusCode? statusCode, Exception? ex = null) : Exception(string.Join(", ", errors))
    {
        public IList<string> Errors { get; set; } = errors;
        public HttpStatusCode? StatusCode { get; set; } = statusCode;
        public Exception? InnerException { get; set; } = ex;
        public string Service = "";
    }
}
