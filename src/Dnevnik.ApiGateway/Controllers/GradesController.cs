using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;
using Dnevnik.ApiGateway.Extensions;
using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.Journal.Models;
using Dnevnik.ApiGateway.Services.Schedule.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.ApiGateway.Controllers;

[Authorize]
public class GradesController(IApiServiceFactory apiServiceFactory) : BaseController
{
    [HttpGet("schedule/{id:guid}/grades/user")]
    public async Task<GradesResponse> GetUserGrades(Guid id)
    {
        var userId = HttpContext.GetIdFromClaim();
        
        var journalApiService = apiServiceFactory.CreateJournalApiService(nameof(GradesController));
        var marks = await journalApiService.GetMarks(new FiltersRequest { LessonId = id, UserId = userId });
        
        var usersApiService = apiServiceFactory.CreateUsersApiService(nameof(GradesController));
        var student = (await usersApiService.GetUserInfoAsync(userId)).MapToStudent();
        
        return new GradesResponse
        {
            Student = student,
            Grades = marks.Select(a => new GradeDto { Comment = a.Comment, Grade = a.Mark }).ToArray()
        };
    }

    [HttpGet("schedule/{id:guid}/grades")]
    public async Task<GradesResponse[]> GetAllStudentsGrades(Guid id)
    {
        var journalApiService = apiServiceFactory.CreateJournalApiService(nameof(GradesController));
        var marks = (await journalApiService.GetMarks(new FiltersRequest { LessonId = id }))
            .GroupBy(a => a.UserId);
        
        var usersApiService = apiServiceFactory.CreateUsersApiService(nameof(GradesController));
        var result = new List<GradesResponse>();
        foreach (var group in marks)
        {
            var student = (await usersApiService.GetUserInfoAsync(group.Key)).MapToStudent();
            result.Add(new GradesResponse() { Student = student, Grades = group.Select(a => new GradeDto() { Comment = a.Comment, Grade = a.Mark }).ToArray()});
        }

        return result.ToArray();
    }

    [HttpPut("schedule/{id:guid}/grades")]
    public async Task UpdateGrades(Guid id, StudentGrade[] grades)
    {
        var scheduleApiService = apiServiceFactory.CreateScheduleApiService(nameof(GradesController));
        var lesson = await scheduleApiService.GetLesson(id);

        var journalApiService = apiServiceFactory.CreateJournalApiService(nameof(GradesController));
        await journalApiService.CreateMarks(ConvertToCreateMark(lesson, grades));
    }

    private CreateMarkRequest[] ConvertToCreateMark(Lesson lesson, StudentGrade[] grades)
    {
        return grades.SelectMany(a => a.Grades.Select(b => new CreateMarkRequest()
        {
            Comment = b.Comment,
            Mark = b.Grade!,
            LessonId = lesson.Id,
            UserId = a.StudentId
        }))
        .ToArray();
    }
}