using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Services.Tasks;

public class TasksApiService(IHttpService httpService) : BaseApiService, ITasksApiService
{
    public async Task<Task> CreateTask(CreateTask task)
    {
        var responce = await httpService.PostAsync(new HttpPostRequest
        {
            Route = "task/create-with-assignment",
            Body = JsonSerialize(task)
        });
        return JsonDeserialize<Task>(responce);
    }

    public async Task<Task?> GetTaskOrDefault(Guid id)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest
        {
            Route = $"task/{id}"
        });
        
        return JsonDeserialize<Task>(response);
    }

    public async Task<Task> UpdateTask(Task updatedTask)
    {
        var responce = await httpService.PostAsync(new HttpPostRequest
        {
            Route = "task/assignment-update",
            Body = JsonSerialize(updatedTask)
        });
        return JsonDeserialize<Task>(responce);
    }

    public async void DeleteTask(Guid id)
    {
        await httpService.GetAsync(new DeleteHttpRequest
        {
            Route = $"task/{id}/delete"
        });
    }
}