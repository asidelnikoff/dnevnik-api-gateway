using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;

namespace Dnevnik.ApiGateway.Services.Schedule;

public interface IScheduleApiService
{
    Task<Lesson[]> GetSummarySchedule(ScheduleRequest parameters);
    Task<Lesson[]> GetUserSchedule(string className, ScheduleRequest parameters);
    Task<Lesson> CreateLesson(CreateLesson lessonInfo);
    Task<Lesson> UpdateLesson(Guid lessonId, CreateLesson lessonInfo);
    Task DeleteLesson(Guid lessonId);
    Task<Lesson> GetLesson(Guid id);

    Task SetMark(MarkInfo markInfo);
}