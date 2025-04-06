using Dnevnik.ApiGateway.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();