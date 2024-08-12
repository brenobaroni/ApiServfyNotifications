using ApiServfyNotifications.Data.Configurations;
using ApiServfyNotifications.Data.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiServfyNotifications.Tests
{
    public class NotificationEndPointsTest
    {
        private ServfyNotificationContext _context;

        public NotificationEndPointsTest() 
        {
            var databaseSettings = DatabaseSettings.Create(GetIConfigurationRoot());
            var builder = new DbContextOptionsBuilder<ServfyNotificationContext>();
            builder.UseNpgsql(databaseSettings.ConnectionString, b => b.MigrationsAssembly("ApiServfyNotifications.Migrations"));
            _context = new ServfyNotificationContext(builder.Options);
        }

        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        //[Fact]
        //public async Task GetUserAsync_Should_Return_User()
        //{
        //    var options = new DbContextOptionsBuilder<ServfyNotificationContext>()
        //    .UseNpgsql()
        //    .Options;
            
        //    var userService = new(_context);
        //    var result = await userService.GetUserAsync(1);

        //    result.Id.Should().Be(1);
        //}
    }
}