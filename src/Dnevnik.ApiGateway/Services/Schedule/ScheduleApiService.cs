using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

namespace Dnevnik.ApiGateway.Services.Schedule;

public class ScheduleApiService(IHttpService httpService) : BaseApiService, IScheduleApiService
{
    public Lesson[] GetSummarySchedule(ScheduleRequest parameters)
    {
        throw new NotImplementedException();
    }

    public Task<Lesson[]> GetUserSchedule(Guid uesrId, Role role)
    {
        throw new NotImplementedException();
    }

    public Lesson CreateLesson(CreateLesson lessonInfo)
    {
        throw new NotImplementedException();
    }

    public Lesson UpdateLesson(Guid lessonId, CreateLesson lessonInfo)
    {
        throw new NotImplementedException();
    }

    public void DeleteLesson(Guid lessonId)
    {
        throw new NotImplementedException();
    }

    public void SetMark(MarkInfo markInfoInfo)
    {
        throw new NotImplementedException();
    }
}