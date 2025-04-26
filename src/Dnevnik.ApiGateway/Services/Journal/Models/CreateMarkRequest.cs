namespace Dnevnik.ApiGateway.Services.Journal.Models;

public class CreateMarkRequest
{
    public required Guid UserId { get; set; }
    public required Guid LessonId { get; set; }
    public required string Mark { get; set; }
    public string? Comment { get; set; }
}