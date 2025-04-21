namespace Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;

public class AssignTaskRequest
{
    public required AssignToRequest[] AssignTo { get; set; }
    public Guid TemplateTaskId { get; set; }
}