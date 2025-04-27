namespace Dnevnik.ApiGateway.Services.Tasks.Models;

public class ClassTaskInfo
{
    public Guid LessonId { get; set; }
    public Guid TaskId { get; set; }
    public string? Payload { get; set; }
    public DateTime Deadline { get; set; }
    public Guid TaskTemplateId { get; set; }
}