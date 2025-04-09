using Dnevnik.ApiGateway.Infrastructure.Configuration;
using Dnevnik.ApiGateway.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();

var app = builder.Build();

app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();