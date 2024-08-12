using ApiServfyNotifications.Application.Handlers.BaseResponse;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Response
{
    public class ResponseSendEmail : BaseRestResponse<dynamic>
    {
        public ResponseSendEmail(int httpStatus, string message, bool success) : base(httpStatus, message, success)
        {
            this.data = success;
            this.success = success;
        }

        public bool success {  get; set; }
    }


}
