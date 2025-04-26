using System.Text.Json;

using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

namespace Dnevnik.ApiGateway.Services.Schedule;

public class ScheduleApiService(IHttpService httpService) : BaseApiService, IScheduleApiService
{
    private const string ScheduleRoute = "Schedule";
    private const string LessonRoute = "Lesson";
    private const string LessonCreateWithRepeatRoute = $"{LessonRoute}/CreateWithRepeats";

    private const string DateFormat = "yyyy-MM-dd";
    
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<Lesson[]> GetSummarySchedule(ScheduleRequest parameters)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest
        {
            Route = $"{ScheduleRoute}?{CreateScheduleQuery(parameters)}"
        });

        return JsonDeserialize<Lesson[]>(response);
    }

    public async Task<Lesson[]> GetUserSchedule(string className, ScheduleRequest parameters)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest
        {
            Route = $"{ScheduleRoute}/class/{className}?{CreateScheduleQuery(parameters)}" 
        });

        return JsonDeserialize<Lesson[]>(response).ToArray();
    }

    public async Task<Lesson> CreateLesson(CreateLesson lessonInfo)
    {
        var response = await httpService.PostAsync(new HttpWithBodyRequest
        {
            Route = $"{LessonCreateWithRepeatRoute}{lessonInfo.StartPeriod.ToString(DateFormat)}?{CreatePostQuery(lessonInfo)}",
            Body = JsonSerialize(lessonInfo)
        });

        var result = JsonDeserialize<Guid[]>(response);
        return await GetLesson(result.First());
    }

    public async Task<Lesson> UpdateLesson(Guid lessonId, CreateLesson lessonInfo)
    {
        await httpService.PutAsync(new HttpWithBodyRequest
        {
            Route = $"{LessonRoute}/{lessonId}?{CreatePutQuery(lessonInfo)}",
            Body = JsonSerialize(lessonInfo)
        });

        return await GetLesson(lessonId);
    }

    public async Task DeleteLesson(Guid lessonId)
    {
        await httpService.DeleteAsync(new HttpWithBodyRequest
        {
            Route = $"{LessonRoute}/{lessonId}"
        });
    }

    public async Task SetMark(MarkInfo markInfo)
    {
        await httpService.PostAsync(new HttpWithBodyRequest()
        {
            Route = $"/Mark?{CreateMarkQuery(markInfo)}"
        });
    }

    public async Task<Lesson> GetLesson(Guid id)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest
        {
            Route = $"{LessonRoute}/{id}"
        });

        return JsonDeserialize<Lesson>(response);
    }
    
    private string CreateScheduleQuery(ScheduleRequest parameters) =>
        $"startTime={parameters.StartTime.ToString("hh:mm:ss")}" +
        $"&endTime={parameters.EndTime.ToString("hh:mm:ss")}" +
        $"&startDate={parameters.StartDate.ToString(DateFormat)}" +
        $"&endDate={parameters.EndDate.ToString(DateFormat)}";

    private string CreatePutQuery(CreateLesson request) =>
        $"subject={request.Subject}" +
        $"&userId={request.UserId}" +
        $"&className={request.ClassName}" +
        (request.TaskId is not null ? $"&taskId={request.TaskId}" : "");

    private string CreatePostQuery(CreateLesson lessonInfo) =>
        $"{CreateWeekDaysParam(lessonInfo.DayWeek)}&endPeriod={lessonInfo.EndPeriod.ToString(DateFormat)}";
    private string CreateWeekDaysParam(WeekDay[] weekDays) => weekDays.Aggregate("", (current, weekDay) => current + $"days={(int)weekDay}&").Trim('&');

    private string CreateMarkQuery(MarkInfo markInfo) =>
        $"Date={markInfo.Date}" +
        $"LessonID={markInfo.LessonId}" +
        $"TeacherID={markInfo.TeacherId}" +
        $"StudentID={markInfo.StudentId}" +
        $"Mark={markInfo.Mark ?? 0}" +
        $"Comment={markInfo.Comment ?? ""}";
    
    protected override string JsonSerialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, s_jsonSerializerOptions);
    }

    protected override T JsonDeserialize<T>(string value)
    {
        return JsonSerializer.Deserialize<T>(value, s_jsonSerializerOptions)!;
    }
}