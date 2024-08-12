using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Application.Handlers.BaseResponse
{
    public class BaseRestResponse<T> where T : class
    {
        public BaseRestResponse(int httpStatus, string message, bool success) { this.httpStatus = httpStatus; this.message = message; this.success = success; }

        public int httpStatus { get; set; }
        public T? data {  get; set; }
        public string message { get; set; } = string.Empty;
        public bool success { get; set; }

    }
}
