using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class UsersController : BaseController
{
    [HttpPut("users/me")]
    public IActionResult PutNewPassword(ChangePasswordRequest request) => throw new NotImplementedException();

    [HttpPost("users/students")]
    public Student PostCreateNewStudent(CreateStudentRequest request) => throw new NotImplementedException();
    
    [HttpPost("users/teachers")]
    public Student PostCreateNewTeacher(CreateTeacherRequest request) => throw new NotImplementedException();

    [HttpGet("users/students")]
    public StudentInfoResponse[] GetStudents(
        [FromQuery] string? @class,
        [FromQuery] string? search,
        [FromQuery] StudentsSort sort) => throw new NotImplementedException();
    
    [HttpGet("users/stuff")]
    public StuffInfoResponse[] GetStuff(
        [FromQuery] Role role,
        [FromQuery] string? search,
        [FromQuery] StudentsSort sort) => throw new NotImplementedException();
}