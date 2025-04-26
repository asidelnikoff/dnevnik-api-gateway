using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

[Authorize]
public class ScheduleController(
    IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpGet("schedule")]
    public async Task<ScheduleItem[]> GetSchedule(
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        [FromQuery] TimeOnly startTime,
        [FromQuery] TimeOnly endTime)
    {
        var id = HttpContext.GetClassFromClaim();
        return (await apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
                .GetUserSchedule(id!,
                    new ScheduleRequest
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        StartTime = startTime,
                        EndTime = endTime
                    }))
            .Select(async a => await MapToScheduleItem(a, startDate, endDate))
            .Select(a => a.Result)
            .ToArray();
    }

    [HttpGet("summary")]
    public async Task<Dictionary<string, Dictionary<DateOnly, IEnumerable<KeyValuePair<TimeOnly, ScheduleItem?>>>>> GetSummarySchedule(
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        [FromQuery] TimeOnly startTime,
        [FromQuery] TimeOnly endTime,
        [FromQuery] string[]? classes)
    {
        IEnumerable<Lesson> allSchedule = await apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
            .GetSummarySchedule(new ScheduleRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                StartTime = startTime,
                EndTime = endTime
            });

        if (classes is not null and not [])
        {
            allSchedule = allSchedule.Where(a => classes.Contains(a.ClassName));
        }

        return allSchedule.GroupBy(a => a.ClassName)
            .ToDictionary(a => a.Key,
                a => a.GroupBy(b => b.Date)
                    .ToDictionary(c => c.Key,
                        c => ScheduleConstants.LessonTimes.Select(t => new KeyValuePair<TimeOnly,Lesson?>(t.start, c.FirstOrDefault(l => l.StartTime == t.start))))
                    .ToDictionary(d => d.Key,
                        d => d.Value.Select<KeyValuePair<TimeOnly, Lesson?>, KeyValuePair<TimeOnly, ScheduleItem?>>(l =>
                            l.Value is null ? new KeyValuePair<TimeOnly, ScheduleItem?>(l.Key, null) : new KeyValuePair<TimeOnly, ScheduleItem?>(l.Key, MapToScheduleItem(l.Value, startDate, endDate).Result))));
    }
    
    [HttpPost("schedule")]
    public async Task<ScheduleItem> CreateNewScheduleItem(CreateScheduleItemRequest request)
    {
        var lesson = await apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
            .CreateLesson(request.MapToCreateLesson());

        return await MapToScheduleItem(lesson, request.StartDate, request.EndDate);
    }

    [HttpPut("schedule/{id}")]
    public async Task<ScheduleItem> UpdateScheduleItem(Guid id, CreateScheduleItemRequest request)
    {
        var lesson = await apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
            .UpdateLesson(id, request.MapToCreateLesson());

        return await MapToScheduleItem(lesson, request.StartDate, request.EndDate);
    }

    [HttpDelete("schedule/{id}")]
    public IActionResult DeleteScheduleItem(Guid id)
    {
        apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController)).DeleteLesson(id);
        
        return Ok();
    }

    private async Task<ScheduleItem> MapToScheduleItem(Lesson lesson, DateOnly startDate, DateOnly endDate) => new ScheduleItem
    {
        Id = lesson.Id,
        Class = lesson.ClassName,
        Teacher = await GetTeacherInfo(lesson),
        Homework = await GetHomeworkContent(lesson),
        StartTime = lesson.StartTime.ToString("hh:mm"),
        EndTime = lesson.EndTime.ToString("hh:mm"),
        Subject = lesson.Subject,
        StartDate = startDate,
        EndDate = endDate,
        Grades = null, // todo fill grades
    };

    private async Task<string?> GetHomeworkContent(Lesson lesson)
    {
        return lesson.TaskID is null 
            ? null 
            : (await apiServiceFactory.CreateTasksApiService(nameof(ScheduleController))
                .GetTaskOrDefault(lesson.TaskID.Value, lesson.ClassName))?
            .Payload;
    }

    private async Task<Teacher> GetTeacherInfo(Lesson lesson)
    {
        return (await apiServiceFactory.CreateUsersApiService(nameof(ScheduleController))
                .GetUserInfoAsync(lesson.UserId))
            .MapToTeacher();
    }
}