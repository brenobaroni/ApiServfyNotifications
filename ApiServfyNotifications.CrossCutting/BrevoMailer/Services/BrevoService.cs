using ApiServfyNotifications.Application.Exceptions;
using ApiServfyNotifications.CrossCutting.BrevoMailer.Contracts;
using ApiServfyNotifications.CrossCutting.Models;
using ApiServfyNotifications.Data.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.CrossCutting.BrevoMailer.Services
{
    public class BrevoService : IBrevoService
    {
        private readonly HttpClient _httpClient;
        private readonly ServfyNotificationContext _context;
        public BrevoService(HttpClient httpClient, ServfyNotificationContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<bool> SendEmailByTemplateApi(BrevoSmtpApiSendEmailByTemplateRequest request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.PostAsJsonAsync("/v3/smtp/email", request, cancellationToken);
                if (response.IsSuccessStatusCode)
                    return response.IsSuccessStatusCode;
                else
                    throw new AppCrossCuttingException([await response.Content.ReadAsStringAsync()], response.StatusCode);

            }
            catch (Exception ex) { throw new AppCrossCuttingException([""], System.Net.HttpStatusCode.BadRequest); }
            finally {
                if (response != null)
                    await LogEmailBrevo(await response.Content.ReadAsStringAsync(), response.IsSuccessStatusCode); 
            }

        }

        public async Task LogEmailBrevo(dynamic data, bool success)
        {
            var emailLog = await _context.emailLogs.AddAsync(new Domain.Entities.EmailLogs()
            {
                data = data,
                emailService = "Brevo",
                success = success,
            });

            await _context.SaveChangesAsync();
        }

    }
}
