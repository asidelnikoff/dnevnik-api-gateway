using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

namespace Dnevnik.ApiGateway.Services.Schedule;

public interface IScheduleApiService
{
    Lesson[] GetSummarySchedule(ScheduleRequest parameters);
    Lesson[] GetUserSchedule(Guid uesrId, Role role);
    Lesson CreateLesson(CreateLesson lessonInfo);
    Lesson UpdateLesson(Guid lessonId, CreateLesson lessonInfo);
    void DeleteLesson(Guid lessonId);

    void SetMark(MarkInfo markInfoInfo);
}