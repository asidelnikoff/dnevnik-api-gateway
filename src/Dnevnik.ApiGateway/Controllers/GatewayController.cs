using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Requests;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

public class GatewayController : BaseController
{
    [HttpPost("auth/login")]
    public LoginResponse PostLogin(LoginRequest request) => throw new NotImplementedException();

    [HttpPut("users/me")]
    public IActionResult PutNewPassword(ChangePasswordRequest request) => throw new NotImplementedException();

    [HttpPost("users/students")]
    public Student PostCreateNewStudent(CreateStudentRequest request) => throw new NotImplementedException();
    
    [HttpPost("users/teachers")]
    public Student PostCreateNewTeacher(CreateTeacherRequest request) => throw new NotImplementedException();
}