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
    
    public async Task<Task?> GetTaskOrDefault(Guid id, string className)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest
        {
            Route = $"task/get-by-class?class={className}"
        });

        var deserialized = JsonDeserialize<ClassTasks>(response);

        if (deserialized.Tasks is null or [])
        {
            return null;
        }
        
        var task = deserialized.Tasks
            .First(a => a.TaskId == id);
        
        return JsonDeserialize<Task>(JsonSerialize(task));
    }

    public async Task<Task> UpdateTask(UpdateTaskRequest request)
    {
        var response = await httpService.PutAsync(new HttpWithBodyRequest
        {
            Route = "task/assignment-update",
            Body = JsonSerialize(request)
        });
        
        return await GetTaskOrDefault(request.ClassTaskId, request.Class) ?? throw new Exception("Not found task");
    }

    public async System.Threading.Tasks.Task DeleteTask(Guid id)
    {
        await httpService.DeleteAsync(new HttpWithBodyRequest
        {
            Route = $"task/assignment-delete",
            Body = JsonSerialize(new { ClassTaskId = id })
        });
    }
}