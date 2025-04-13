using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
using Dnevnik.ApiGateway.Controllers.Exceptions;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Users.Models;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class UsersController(IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpPut("users/me")]
    public IActionResult PutNewPassword(ChangePasswordRequest request)
    {
        var apiRequest = new CreateUser
        {
            Password = request.NewPassword,
            Email = request.NewLogin
        };
        apiServiceFactory.CreateUsersApiService(nameof(UsersController)).UpdateUserInfoAsync(Guid.Empty, apiRequest);

        return Ok();
    }

    [HttpPost("users/students")]
    public async Task<Student> PostCreateNewStudent(CreateStudentRequest request)
    {
        var answer = await apiServiceFactory.CreateUsersApiService(nameof(UsersController))
            .CreateUserAsync(new CreateUser()
        {
            Name = request.FirstName,
            Surname = request.LastName,
            Patronymic = request.MiddleName,
            Email = request.Login,
            Password = request.Password,
            ClassName = request.Class,
            Type = UserType.Student
        });

        return new Student
        {
            Id = answer.Id,
            FirstName = answer.Name,
            LastName = answer.Surname,
            MiddleName = answer.Patronymic,
            Login = answer.Email,
            Class = answer.ClassName ?? throw new ClassMissingException($"{answer.Name} {answer.Surname} {answer.Patronymic}"),
            Role = answer.Type switch
            {
                UserType.Student => Role.Student,
                _ => throw new ArgumentOutOfRangeException(nameof(answer), answer.Type, null)
            }
        };
    }

    [HttpPost("users/teachers")]
    public async Task<Teacher> PostCreateNewTeacher(CreateTeacherRequest request)
    {
        var answer = await apiServiceFactory.CreateUsersApiService(nameof(UsersController))
            .CreateUserAsync(new CreateUser
            {
                Name = request.FirstName,
                Surname = request.LastName,
                Patronymic = request.MiddleName,
                Email = request.Login,
                Password = request.Password,
                SubjectName = request.Subject,
                Type = request.Role switch
                {
                    Role.Teacher => UserType.Teacher,
                    Role.Headteacher => UserType.Headteacher,
                    _ => throw new UnsupportedRoleException(request.Role)
                }
            });

        return answer.MapToTeacher();
    }

    [HttpGet("users/students")]
    public StudentInfoResponse[] GetStudents(
        [FromQuery] string? @class,
        [FromQuery] string? search,
        [FromQuery] StudentsSort sort)
    {
        return apiServiceFactory.CreateUsersApiService(nameof(UsersController))
            .GetUsersList()
            .Select(a => new StudentInfoResponse
            {
                FullName = $"{a.Name} {a.Surname} {a.Patronymic}".Trim(),
                Class = a.ClassName ?? throw new ClassMissingException($"{a.Name} {a.Surname} {a.Patronymic}")
            })
            .ToArray();
    }

    [HttpGet("users/stuff")]
    public StuffInfoResponse[] GetStuff(
        [FromQuery] Role role,
        [FromQuery] string? search,
        [FromQuery] StudentsSort sort)
    {
        return apiServiceFactory.CreateUsersApiService(nameof(UsersController))
            .GetUsersList()
            .Select(a => new StuffInfoResponse
            {
                FullName = $"{a.Name} {a.Surname} {a.Patronymic}".Trim(),
                Subject = a.SubjectName ?? throw new SubjectMissingException($"{a.Name} {a.Surname} {a.Patronymic}")
            })
            .ToArray();
    }
}