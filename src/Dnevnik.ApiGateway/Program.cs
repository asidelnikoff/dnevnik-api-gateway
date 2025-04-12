using Dnevnik.ApiGateway.Infrastructure.Configuration;
using Dnevnik.ApiGateway.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();

var app = builder.Build();

app.UseMiddleware<RequestsMetricMiddleware>();
app.UseExceptionMiddleware();
app.UseHttpLogging();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapPrometheusScrapingEndpoint();
app.MapControllers();

app.Run();