using ApiServfyNotifications.CrossCutting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.CrossCutting.BrevoMailer.Contracts
{
    public interface IBrevoService
    {
        Task<bool> SendEmailByTemplateApi(BrevoSmtpApiSendEmailByTemplateRequest request, CancellationToken cancellationToken);
    }
}
