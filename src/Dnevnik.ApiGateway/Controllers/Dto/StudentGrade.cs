﻿namespace Dnevnik.ApiGateway.Controllers.Dto;

public class StudentGrade
{
    public Guid StudentId { get; init; }
    public int? LessonGrade { get; init; }
    public int? HomeworkGrade { get; init; }
}