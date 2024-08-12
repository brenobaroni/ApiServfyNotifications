using ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Application.Services.Contracts
{
    public interface IEmailNotificationService
    {
        Task<bool> SendNotificationEmailAsync(RequestSendEmail request, CancellationToken cancellationToken);
    }
}
