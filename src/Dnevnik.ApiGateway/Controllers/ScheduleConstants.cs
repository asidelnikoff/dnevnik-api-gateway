namespace Dnevnik.ApiGateway.Controllers;

public static class ScheduleConstants
{
    public static (TimeOnly start, TimeOnly end)[] LessonTimes = new[]
    {
        (TimeOnly.Parse("08:00:00"), TimeOnly.Parse("08:40:00")),
        (TimeOnly.Parse("08:50:00"), TimeOnly.Parse("09:30:00")),
        (TimeOnly.Parse("09:50:00"), TimeOnly.Parse("10:30:00")),
        (TimeOnly.Parse("10:50:00"), TimeOnly.Parse("11:30:00")),
        (TimeOnly.Parse("11:50:00"), TimeOnly.Parse("12:30:00")),
        (TimeOnly.Parse("12:40:00"), TimeOnly.Parse("13:20:00")),
        (TimeOnly.Parse("13:40:00"), TimeOnly.Parse("14:20:00"))
    };
}