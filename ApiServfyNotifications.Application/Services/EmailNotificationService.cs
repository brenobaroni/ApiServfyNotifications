using ApiServfyNotifications.Application.Exceptions;
using ApiServfyNotifications.Application.Handlers.Notification.CommandSendEmail.Command;
using ApiServfyNotifications.Application.Services.Contracts;
using ApiServfyNotifications.CrossCutting.BrevoMailer.Contracts;
using ApiServfyNotifications.CrossCutting.Models;
using ApiServfyNotifications.Data.Context;
using ApiServfyNotifications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Application.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        public readonly IConfiguration _configuation;
        public readonly IBrevoService _brevoService;
        public readonly ServfyNotificationContext _servfyNotificationContext;
        public readonly IMemoryCache _memoryCache;

        public EmailNotificationService(IConfiguration configuation, ServfyNotificationContext servfyNotificationContext, IBrevoService brevoService, IMemoryCache memoryCache)
        {
            _configuation = configuation;
            _servfyNotificationContext = servfyNotificationContext;
            _brevoService = brevoService;
            _memoryCache = memoryCache;
        }


        public async Task<bool> SendNotificationEmailAsync(RequestSendEmail request, CancellationToken cancellationToken)
        {
            var emailConfigurationsCache = _memoryCache.Get<List<EmailConfigurations>>("EmailConfigurations"); // Try Get From Cache First

            List<EmailConfigurations> emailConfigurations = emailConfigurationsCache ??  await _servfyNotificationContext.emailConfigurations.ToListAsync();

            if (emailConfigurationsCache is null && emailConfigurations.Any())
                _memoryCache.Set<List<EmailConfigurations>>("EmailConfigurations", emailConfigurations, DateTime.Now.AddMinutes(15));

            if (!emailConfigurations.Any())
                throw new AppException(["Miss Configurations for Email Devilery..."], System.Net.HttpStatusCode.InternalServerError);

            string defaultDelivery = emailConfigurations.FirstOrDefault(f => f.key == "EmailServiceDefault")?.value ?? "";

            if (string.IsNullOrEmpty(defaultDelivery))
                throw new AppException(["Miss Configurations for Default Email Devilery..."], System.Net.HttpStatusCode.InternalServerError);


            switch (defaultDelivery)
            {
                case "Brevo":
                    var templateId = emailConfigurations.FirstOrDefault(f => f.key == request.NotificationType)?.value ?? "";
                    if (string.IsNullOrEmpty(templateId))
                        throw new AppException(["Miss Configurations for Template Email Devilery..."], System.Net.HttpStatusCode.InternalServerError);

                    var brevoRequest = new BrevoSmtpApiSendEmailByTemplateRequest()
                    {
                        Params = request.Params,
                        TemplateId = int.Parse(templateId),
                        To = request.To.Select(s => new BrevoSmtpApiSendEmailTo() { Email = s.Email, Name = s.Name }).ToList()
                    };

                    var response = await _brevoService.SendEmailByTemplateApi(brevoRequest, cancellationToken);

                    return response;
            }


            throw new AppException(["Miss Configurations for Default Email Devilery..."], System.Net.HttpStatusCode.InternalServerError);
        }

    }
}
