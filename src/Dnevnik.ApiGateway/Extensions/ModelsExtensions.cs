using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Exceptions;
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
        Login = user.Login,
        Role = user.Type switch
        {
            UserType.Teacher => Role.Teacher,
            UserType.Headteacher => Role.Headteacher,
            _ => throw new ArgumentOutOfRangeException(nameof(user), user.Type, null)
        },
        Subject = user.Subject ?? ""
    };
    
    public static Controllers.Dto.User MapToUser(this User user) => new()
    {
        Id = user.Id,
        FirstName = user.Name,
        LastName = user.Surname,
        MiddleName = user.Patronymic,
        Login = user.Login,
        Role = user.Type switch
        {
            UserType.Teacher => Role.Teacher,
            UserType.Headteacher => Role.Headteacher,
            _ => throw new ArgumentOutOfRangeException(nameof(user), user.Type, null)
        }
    };
    
    public static Student MapToStudent(this User user) => new()
    {
        Id = user.Id,
        FirstName = user.Name,
        LastName = user.Surname,
        MiddleName = user.Patronymic,
        Login = user.Login,
        Role = user.Type switch
        {
            UserType.Teacher => Role.Teacher,
            UserType.Headteacher => Role.Headteacher,
            _ => throw new ArgumentOutOfRangeException(nameof(user), user.Type, null)
        },
        Class = user.ClassName ?? ""
    };
    
    public static CreateLesson MapToCreateLesson(this CreateScheduleItemRequest request) => new()
    {
        Subject = request.Subject,
        ClassName = request.Class,
        UserId = request.TeacherId,
        DayWeek = request.WeekDays,
        StartPeriod = request.StartDate,
        EndPeriod = request.EndDate,
        StartTime = TimeOnly.Parse(request.StartTime),
        EndTime = TimeOnly.Parse(request.EndTime)
    };

    public static UserType MapToUserType(this Role role) => role switch
    {
        Role.Student => UserType.Student,
        Role.Teacher => UserType.Teacher,
        Role.Headteacher => UserType.Headteacher,
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };
}