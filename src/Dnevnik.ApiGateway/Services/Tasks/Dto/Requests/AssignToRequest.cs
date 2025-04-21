namespace Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;

public class AssignToRequest
{
    public required string Class { get; set; }
    public Guid LessonId { get; set; }
}