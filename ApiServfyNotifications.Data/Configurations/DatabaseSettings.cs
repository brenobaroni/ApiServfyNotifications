using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServfyNotifications.Data.Configurations
{
    public sealed class DatabaseSettings
    {
        public static DatabaseSettings Create(IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings();
            databaseSettings.ConnectionString = configuration.GetConnectionString("Base");
            return databaseSettings;
        }

        public string? ConnectionString { get; set; }
    }
}
