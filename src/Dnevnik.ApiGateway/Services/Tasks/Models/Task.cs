namespace Dnevnik.ApiGateway.Services.Tasks.Models;

public class Task
{
    public Guid Id { get; init; }
    public DateTime Deadline { get; init; }
    public string? Payload { get; init; }
}