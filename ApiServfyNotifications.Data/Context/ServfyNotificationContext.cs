using ApiServfyNotifications.Domain;
using ApiServfyNotifications.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace ApiServfyNotifications.Data.Context
{
    public class ServfyNotificationContext : DbContext
    {
        private static readonly ILoggerFactory DebugLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        public IHttpContextAccessor? HttpContext { get; }
        private readonly IHostEnvironment? _env;
        private readonly IMemoryCache _cache;

        public ServfyNotificationContext(DbContextOptions<ServfyNotificationContext> options) : base(options)
        {

        }

        public ServfyNotificationContext(DbContextOptions<ServfyNotificationContext> options, IHttpContextAccessor? httpContext, IHostEnvironment? env, IMemoryCache memoryCache)
            : base(options)
        {
            HttpContext = httpContext;
            _env = env;
            _cache = memoryCache;
        }

        public DbSet<EmailConfigurations> emailConfigurations { get; set; }
        public DbSet<EmailLogs> emailLogs { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServfyNotificationContext).Assembly);
            //modelBuilder.SeedTemplates();
            //modelBuilder.SeedMenu();
            //modelBuilder.SeedRoles();
            //modelBuilder.SeedRoleMenu();
        }

    }
}
