using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
using Dnevnik.ApiGateway.Controllers.Exceptions;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Users.Dto;
using Dnevnik.ApiGateway.Services.Users.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

[Authorize]
public class UsersController(IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpPut("users/me")]
    public async Task PutNewPassword(ChangePasswordRequest request)
    {
        var apiRequest = new CreateUser
        {
            Password = request.NewPassword,
            Login = request.NewLogin
        };
        await apiServiceFactory.CreateUsersApiService(nameof(UsersController))
            .UpdateUserInfoAsync(HttpContext.GetIdFromClaim(), apiRequest);
    }

    [HttpPost("users/students")]
    public async Task<Student> PostCreateNewStudent(CreateStudentRequest request)
    {
        var answer = await apiServiceFactory.CreateUsersApiService(nameof(UsersController))
            .CreateUserAsync(new CreateUser
            {
                Name = request.FirstName,
                Surname = request.LastName,
                Patronymic = request.MiddleName,
                Login = request.Login,
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
            Login = answer.Login,
            Class = answer.ClassName ??
                    throw new ClassMissingException($"{answer.Name} {answer.Surname} {answer.Patronymic}"),
            Role = answer.Type switch
            {
                UserType.Student => Role.Student,
                _ => throw new ArgumentOutOfRangeException(nameof(answer), answer.Type, null)
            }
        };
    }

    [HttpDelete("users/students/{id:guid}")]
    [HttpDelete("users/teachers/{id:guid}")]
    public async Task DeleteStudent(Guid id)
    {
        await apiServiceFactory.CreateUsersApiService(nameof(UsersController))
            .DeleteUserAsync(id);
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
                Login = request.Login,
                Password = request.Password,
                Subject = request.Subject,
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
    public async Task<StudentInfoResponse[]> GetStudents(
        [FromQuery] string? @class,
        [FromQuery] string? firstName,
        [FromQuery] string? lastName,
        [FromQuery] string? middleName,
        [FromQuery] StudentsSort sort,
        [FromQuery] Pagination pagination)
    {
        return (await apiServiceFactory.CreateUsersApiService(nameof(UsersController))
                .GetUsersList(new FilterRequest
                {
                    ClassName = @class,
                    Name = firstName,
                    Surname = lastName,
                    Patronymic = middleName
                }))
            .Select(a => new StudentInfoResponse
            {
                FullName = $"{a.Name} {a.Surname} {a.Patronymic}".Trim(),
                Class = a.ClassName ?? throw new ClassMissingException($"{a.Name} {a.Surname} {a.Patronymic}")
            })
            .SortBy(sort)
            .Skip(pagination.Offset)
            .Take(pagination.Limit)
            .ToArray();
    }

    [HttpGet("users/stuff")]
    public async Task<StuffInfoResponse[]> GetStuff(
        [FromQuery] Role role,
        [FromQuery] string? firstName,
        [FromQuery] string? lastName,
        [FromQuery] string? middleName,
        [FromQuery] string? subject,
        [FromQuery] StuffSort sort,
        [FromQuery] Pagination pagination)
    {
        return (await apiServiceFactory.CreateUsersApiService(nameof(UsersController))
                .GetUsersList(new FilterRequest
                {
                    Name = firstName,
                    Surname = lastName,
                    Patronymic = middleName,
                    Subject = subject,
                    Type = role.MapToUserType()
                }))
            .Select(a => new StuffInfoResponse
            {
                FullName = $"{a.Name} {a.Surname} {a.Patronymic}".Trim(),
                Subject = a.Subject ?? throw new SubjectMissingException($"{a.Name} {a.Surname} {a.Patronymic}")
            })
            .SortBy(sort)
            .Skip(pagination.Offset)
            .Take(pagination.Limit)
            .ToArray();
    }
}