using ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Command;
using ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Response;
using ApiServfyNotifications.Application.Services.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail
{
    public class PostSendEmailCommandHandler : IRequestHandler<RequestSendEmail, ResponseSendEmail>
    {
        private readonly IEmailNotificationService _notificationService;

        public PostSendEmailCommandHandler(IEmailNotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<ResponseSendEmail> Handle(RequestSendEmail request, CancellationToken cancellationToken)
        {
            var response = await _notificationService.SendNotificationEmailAsync(request, cancellationToken);

            if(response)
                return new ResponseSendEmail((int)HttpStatusCode.Created, "", response);

            return new ResponseSendEmail((int)HttpStatusCode.Created, "", response);
        }
    }
}
