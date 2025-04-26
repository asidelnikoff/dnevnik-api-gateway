using System.Text.Json;

using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

namespace Dnevnik.ApiGateway.Services.Schedule;

public class ScheduleApiService(IHttpService httpService) : BaseApiService, IScheduleApiService
{
    public Task<Lesson[]> GetSummarySchedule(ScheduleRequest parameters)
    {
        throw new NotImplementedException();
    }

    public Task<Lesson[]> GetUserSchedule(string className, ScheduleRequest parameters)
    {
        throw new NotImplementedException();
    }

    public Task<Lesson> CreateLesson(CreateLesson lessonInfo)
    {
        throw new NotImplementedException();
    }

    public Task<Lesson> UpdateLesson(Guid lessonId, CreateLesson lessonInfo)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLesson(Guid lessonId)
    {
        throw new NotImplementedException();
    }

    public Task<Lesson> GetLesson(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task SetMark(MarkInfo markInfo)
    {
        throw new NotImplementedException();
    }
}