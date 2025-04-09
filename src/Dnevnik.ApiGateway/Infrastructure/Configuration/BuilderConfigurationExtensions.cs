﻿using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.HttpLogging;

namespace Dnevnik.ApiGateway.Infrastructure.Configuration;

public static class BuilderConfigurationExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.RegisterOptions(builder.Configuration);
        
        services.AddHttpLogging(o =>
        {
            o.CombineLogs = false;
            o.LoggingFields = HttpLoggingFields.All;
        });

        services.AddResponseCompression();
        services.AddRouting();
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
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