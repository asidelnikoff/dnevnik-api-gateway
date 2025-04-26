using Dnevnik.ApiGateway.Infrastructure.Configuration;
using Dnevnik.ApiGateway.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();

var app = builder.Build();

app.UseMiddleware<RequestsMetricMiddleware>();
app.UseExceptionMiddleware();
app.UseHttpLogging();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapPrometheusScrapingEndpoint();
app.MapHealthChecks();
app.MapControllers();

app.Run();