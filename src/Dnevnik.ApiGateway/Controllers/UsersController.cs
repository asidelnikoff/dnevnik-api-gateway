using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
using Dnevnik.ApiGateway.Controllers.Exceptions;
using Dnevnik.ApiGateway.Services.Users;
using Dnevnik.ApiGateway.Services.Users.Models;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class UsersController(IUsersApiService usersApiService) : BaseController
{
    [HttpPut("users/me")]
    public IActionResult PutNewPassword(ChangePasswordRequest request)
    {
        var apiRequest = new CreateUser
        {
            Password = request.NewPassword,
            Email = request.NewLogin
        };
        usersApiService.UpdateUserInfo(Guid.Empty, apiRequest);

        return Ok();
    }

    [HttpPost("users/students")]
    public async Task<Student> PostCreateNewStudent(CreateStudentRequest request)
    {
        var answer = await usersApiService.CreateUserAsync(new CreateUser()
        {
            Name = request.FirstName,
            Surname = request.LastName,
            Patronymic = request.MiddleName,
            Email = request.Login,
            Password = request.Password,
            Type = UserType.Student
        });

        return new Student
        {
            Id = answer.Id,
            FirstName = answer.Name,
            LastName = answer.Surname,
            MiddleName = answer.Patronymic,
            Login = answer.Email,
            Class = answer.ClassName ?? throw new ClassMissingException(),
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
        var answer = await usersApiService.CreateUserAsync(new CreateUser
        {
            Name = request.FirstName,
            Surname = request.LastName,
            Patronymic = request.MiddleName,
            Email = request.Login,
            Password = request.Password,
            Type = request.Role switch
            {
                Role.Teacher => UserType.Teacher,
                Role.Headteacher => UserType.Headteacher,
                _ => throw new UnsupportedRoleException(request.Role)
            }
        });

        return new Teacher
        {
            Id = answer.Id,
            FirstName = answer.Name,
            LastName = answer.Surname,
            MiddleName = answer.Patronymic,
            Login = answer.Email,
            Role = answer.Type switch
            {
                UserType.Teacher => Role.Teacher,
                UserType.Headteacher => Role.Headteacher,
                _ => throw new ArgumentOutOfRangeException(nameof(answer), answer.Type, null)
            },
            Subject = answer.SubjectName ?? throw new SubjectMissingException()
        };
    }

    [HttpGet("users/students")]
    public StudentInfoResponse[] GetStudents(
        [FromQuery] string? @class,
        [FromQuery] string? search,
        [FromQuery] StudentsSort sort)
    {
        return usersApiService.GetUsersList()
            .Select(a => new StudentInfoResponse
            {
                FullName = $"{a.Name} {a.Surname} {a.Patronymic}".Trim(),
                Class = a.ClassName ?? throw new ClassMissingException()
            })
            .ToArray();
    }

    [HttpGet("users/stuff")]
    public StuffInfoResponse[] GetStuff(
        [FromQuery] Role role,
        [FromQuery] string? search,
        [FromQuery] StudentsSort sort)
    {
        return usersApiService.GetUsersList()
            .Select(a => new StuffInfoResponse
            {
                FullName = $"{a.Name} {a.Surname} {a.Patronymic}".Trim(),
                Subject = a.SubjectName ?? throw new SubjectMissingException()
            })
            .ToArray();
    }
}