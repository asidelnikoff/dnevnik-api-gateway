using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Services.Schedule.Dto;
using Dnevnik.ApiGateway.Services.Schedule.Models;
using Dnevnik.ApiGateway.Services.Users.Models;

using User = Dnevnik.ApiGateway.Services.Users.Models.User;

namespace Dnevnik.ApiGateway.Extensions;

public static class ModelsExtensions
{
    public static Teacher MapToTeacher(this User user) => new()
    {
        Id = user.Id,
        FirstName = user.Name,
        LastName = user.Surname,
        MiddleName = user.Patronymic,
        Login = user.Email,
        Role = user.Type switch
        {
            UserType.Teacher => Role.Teacher,
            UserType.Headteacher => Role.Headteacher,
            _ => throw new ArgumentOutOfRangeException(nameof(user), user.Type, null)
        },
        Subject = user.SubjectName ?? ""
    };
    
    public static CreateLesson MapToCreateLesson(this CreateScheduleItemRequest request) => new()
    {
        Subject = new Subject { Name = request.Subject },
        ClassName = request.Class,
        TeacherId = request.TeacherId,
        DayWeek = request.WeekDays,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        StartTime = TimeOnly.Parse(request.StartTime),
        EndTime = TimeOnly.Parse(request.EndTime)
    };
}