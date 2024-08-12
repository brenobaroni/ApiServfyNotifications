using ApiServfyNotifications.CrossCutting.BrevoMailer.Contracts;
using ApiServfyNotifications.CrossCutting.BrevoMailer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiServfyNotifications.Application.Modules
{
    public static class CrossCuttingModule
    {
        public static IServiceCollection AddCrossCuttingModule(this IServiceCollection services, IConfiguration configuration)
        {
            var brevoUri = configuration.GetSection("Brevo:Url").Value;
            var brevoApiKey = configuration.GetSection("Brevo:ApiKey").Value;

            //Services
            services.AddTransient<IBrevoService, BrevoService>();
            services.AddHttpClient<IBrevoService, BrevoService>(client =>
            {
                client.BaseAddress = new Uri(brevoUri);
                client.DefaultRequestHeaders.Add("api-key", brevoApiKey);
            });


            return services;
        }


        
    }
}
