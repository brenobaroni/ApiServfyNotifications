using ApiServfyNotifications.Data.Configurations;
using ApiServfyNotifications.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ApiServfyNotifications.Data.Modules
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ServfyNotificationContext>(options =>
            {
                DatabaseSettings databaseSetting = DatabaseSettings.Create(configuration);
                var mongoClient = new MongoClient(databaseSetting.ConnectionString);
                var dataBase = mongoClient.GetDatabase("servfy-prod");
                options.UseMongoDB(dataBase.Client, dataBase.DatabaseNamespace.DatabaseName);
            }, ServiceLifetime.Singleton);

            services.AddMemoryCache();

            return services;
        }


        //private readonly DbContextOptions<ServfyBaseContext> _options;

        //public InfrastructureModule(IConfiguration configuration) : this(CreateDbOptions(configuration), configuration)
        //{
        //    Configuration = configuration;
        //}

        //public InfrastructureModule(DbContextOptions<ServfyBaseContext> options, IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //    _options = options;
        //}

        //protected override void Load(ContainerBuilder builder)
        //{
        //    builder.RegisterInstance(Options.Create(DatabaseSettings.Create(Configuration)));
        //    builder.RegisterType<ServfyBaseContext>()
        //        .AsSelf()
        //        .InstancePerRequest()
        //        .InstancePerLifetimeScope()
        //        .WithParameter(new NamedParameter("options", _options)).ExternallyOwned();

        //    builder.Register(ctx =>
        //    {
        //        return new MemoryCache(new MemoryCacheOptions());
        //    }).As<IMemoryCache>().SingleInstance();
        //}

        //private static DbContextOptions<ServfyBaseContext> CreateDbOptions(IConfiguration configuration)
        //{
        //    var databaseSettings = DatabaseSettings.Create(configuration);
        //    var builder = new DbContextOptionsBuilder<ServfyBaseContext>();
        //    builder.UseNpgsql(databaseSettings.ConnectionString, b => b.MigrationsAssembly("ApiServfyNotifications.Migrations"));
        //    return builder.Options;
        //}
    }
}
