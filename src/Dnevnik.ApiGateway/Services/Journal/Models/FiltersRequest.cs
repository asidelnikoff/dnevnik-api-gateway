namespace Dnevnik.ApiGateway.Services.Journal.Models;

public class FiltersRequest
{
    public Guid? UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public Guid? LessonId { get; set; }
    public string? Subject { get; set; }
}