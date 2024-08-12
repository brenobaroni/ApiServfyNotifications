using ApiServfyNotifications.Data.Modules;
using ApiServfyNotifications.Extensions.Configurations;
using ApiServfyNotifications.Middleware;
using ApiServfyNotifications.Application;
using Asp.Versioning.ApiExplorer;
using ApiServfyNotifications.Application.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string[]>("Cors") ?? [])
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// DependenciesModules
builder.Services.AddWebModule();
builder.Services.AddInfrastructureModule(builder.Configuration);
builder.Services.AddApplicationServicesModule();
builder.Services.AddCrossCuttingModule(builder.Configuration);
builder.Services.AddVersioningModule();
builder.Services.AddSwaggerGen();
builder.Services.AddNotificationConsumer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }

    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseErrorMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
