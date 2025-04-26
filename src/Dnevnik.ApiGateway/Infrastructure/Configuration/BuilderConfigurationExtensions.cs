using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.HttpLogging;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class BuilderConfigurationExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.RegisterOptions(builder.Configuration);
        
        services.AddHttpLoggingInterceptor<HttpLoggingInterceptor>();
        services.AddHttpLogging(o =>
        {
            o.CombineLogs = false;
            o.LoggingFields = HttpLoggingFields.All;
        });
        
        services.AddCors(options => options.AddPolicy(
            "AllowAllOrigins",
            policyBuilder =>
            {
                policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition");
            }
        ));
        
        services.AddHealthChecks(builder.Configuration);
        services.AddResponseCompression();
        services.AddRouting();
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
            });
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddVersioningApi(builder.Configuration);

        services
            .AddTelemetry(builder.Configuration, builder.Environment.ApplicationName)
            .AddAppServices();
    }
}