namespace Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;

public class CreateTaskTemplateRequest
{
    public DateTime Deadline { get; set; }
    public string? Payload { get; set; }
}