using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class ScheduleController(
    IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpGet("schedule")]
    public ScheduleItem[] GetSchedule(
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        [FromQuery] TimeOnly startTime,
        [FromQuery] TimeOnly endTime)
    {
        return apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
            .GetSummarySchedule(new ScheduleRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                StartTime = startTime,
                EndTime = endTime
            })
            .Select(async a => await MapToScheduleItem(a, startDate, endDate))
            .Select(a => a.Result)
            .ToArray();
    }

    [HttpGet("summary")]
    public ScheduleItem[] GetSummarySchedule(
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate,
        [FromQuery] TimeOnly startTime,
        [FromQuery] TimeOnly endTime)
    {
        return apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
            .GetSummarySchedule(new ScheduleRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                StartTime = startTime,
                EndTime = endTime
            })
            .Select(async a => await MapToScheduleItem(a, startDate, endDate))
            .Select(a => a.Result)
            .ToArray();
    }
    
    [HttpPost("schedule")]
    public async Task<ScheduleItem> CreateNewScheduleItem(CreateScheduleItemRequest request)
    {
        var lesson = apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
            .CreateLesson(request.MapToCreateLesson());

        return await MapToScheduleItem(lesson, request.StartDate, request.EndDate);
    }

    [HttpPut("schedule/{id}")]
    public async Task<ScheduleItem> UpdateScheduleItem(Guid id, CreateScheduleItemRequest request)
    {
        var lesson = apiServiceFactory.CreateScheduleApiService(nameof(ScheduleController))
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
        Teacher = (await apiServiceFactory.CreateUsersApiService(nameof(ScheduleController))
            .GetUserInfoAsync(lesson.TeacherId))
            .MapToTeacher(),
        Homework = (await apiServiceFactory.CreateTasksApiService(nameof(ScheduleController)).GetTaskOrDefault(lesson.Task))?.Payload,
        StartTime = lesson.StartTime.ToString("hh:mm"),
        EndTime = lesson.EndTime.ToString("hh:mm"),
        Subject = lesson.Subject.Name,
        StartDate = startDate,
        EndDate = endDate,
        HomeworkGrade = null, // todo fill homework grade
        LessonGrade = null // todo fill lesson grade
    };
}