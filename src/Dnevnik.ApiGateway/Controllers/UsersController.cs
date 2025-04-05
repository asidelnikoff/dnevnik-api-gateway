using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
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
            Login = request.NewLogin
        };
        usersApiService.UpdateUserInfo(Guid.Empty, apiRequest);
        
        return Ok();
    }

    [HttpPost("users/students")]
    public Student PostCreateNewStudent(CreateStudentRequest request)
    {
        var answer = usersApiService.CreateUser(new CreateUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Login = request.Login,
            Password = request.Password,
            UserType = UserType.Student
        });

        return new Student
        {
            Id = answer.Id,
            FirstName = answer.FirstName,
            LastName = answer.LastName,
            MiddleName = answer.MiddleName,
            Login = answer.Login,
            Class = answer.Class ?? throw new Exception(), // todo custom excpetion
            Role = answer.UserType switch
            {
                UserType.Student => Role.Student,
                _ => throw new ArgumentOutOfRangeException() // todo custom excpetion
            }
        };
    }

    [HttpPost("users/teachers")]
    public Teacher PostCreateNewTeacher(CreateTeacherRequest request)
    {
        var answer = usersApiService.CreateUser(new CreateUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Login = request.Login,
            Password = request.Password,
            UserType = request.Role switch {
                Role.Teacher => UserType.Teacher,
                Role.Headteacher => UserType.Headteacher,
                _ => throw new ArgumentOutOfRangeException() // todo custom exception
            }
        });

        return new Teacher
        {
            Id = answer.Id,
            FirstName = answer.FirstName,
            LastName = answer.LastName,
            MiddleName = answer.MiddleName,
            Login = answer.Login,
            Role = answer.UserType switch
            {
                UserType.Teacher => Role.Teacher,
                UserType.Headteacher => Role.Headteacher,
                _ => throw new ArgumentOutOfRangeException() // todo custom exception
            },
            Subject = answer.Subject ?? throw new Exception() // todo custom exception
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
                FullName = $"{a.FirstName} {a.LastName} {a.MiddleName}".Trim(),
                Class = a.Class ?? throw new Exception() // todo custom exception
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
                FullName = $"{a.FirstName} {a.LastName} {a.MiddleName}".Trim(),
                Subject = a.Subject ?? throw new Exception() // todo custom exception
            })
            .ToArray();
    }
}