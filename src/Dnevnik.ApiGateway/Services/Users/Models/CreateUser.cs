﻿namespace Dnevnik.ApiGateway.Services.Users.Models;

public class CreateUser
{
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Patronymic { get; init; }
    public string? Email { get; init; }
    public UserType Type { get; init; }
    public string? Password { get; init; }
    public string? ClassName { get; init; }
    public string? SubjectName { get; init; }
}