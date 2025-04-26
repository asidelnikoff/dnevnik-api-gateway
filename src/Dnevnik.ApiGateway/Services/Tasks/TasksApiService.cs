using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Tasks.Dto;
using Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;
using Dnevnik.ApiGateway.Services.Tasks.Dto.Responses;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Services.Tasks;

public class TasksApiService(IHttpService httpService) : BaseApiService, ITasksApiService
{
    public async Task<Task> CreateTask(CreateTask task)
    {
        var taskResponse = JsonDeserialize<CreateTaskTemplateResponse>(await httpService.PostAsync(new HttpWithBodyRequest
        {
            Route = "task",
            Body = JsonSerialize(new CreateTaskTemplateRequest
            {
                Deadline = task.Deadline,
                Payload = task.Payload
            })
        }));

        await httpService.PostAsync(new HttpWithBodyRequest
        {
            Route = "task/assignment",
            Body = JsonSerialize(new AssignTaskRequest
            {
                AssignTo = [new AssignToRequest { Class = task.Class, LessonId = task.LessonId }],
                TemplateTaskId = taskResponse.Id
            })
        });

        return await GetTask(taskResponse.Id);
    }

    public async Task<Task> GetTask(Guid id)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest
        {
            Route = $"task/{id}"
        });
        
        return JsonDeserialize<Task>(response);
    }

    public Task<Task?> GetTaskOrDefault(Guid id, string className)
    {
        throw new NotImplementedException();
    }

    public Task<Task> UpdateTask(UpdateTaskRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Task> UpdateTask(Task updatedTask)
    {
        var response = await httpService.PutAsync(new HttpWithBodyRequest
        {
            Route = "task/update",
            Body = JsonSerialize(updatedTask)
        });
        
        return JsonDeserialize<Task>(response);
    }

    public async System.Threading.Tasks.Task DeleteTask(Guid id)
    {
        await httpService.DeleteAsync(new HttpWithBodyRequest
        {
            Route = $"task/{id}/delete"
        });
    }
}