﻿using Microsoft.AspNetCore.HttpLogging;

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
        
        services.AddRouting();
        services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddVersioningApi(builder.Configuration);

        services
            .AddTelemetry(builder.Configuration, builder.Environment.ApplicationName)
            .AddAppServices();
    }
}