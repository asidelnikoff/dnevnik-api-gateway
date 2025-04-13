using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Services.Tasks;

public class TasksApiService(IHttpService httpService) : BaseApiService, ITasksApiService
{
    public Task CreateTask(CreateTask task)
    {
        throw new NotImplementedException();
    }

    public Task<Task?> GetTaskOrDefault(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTask(Task updatedTask)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(Guid id)
    {
        throw new NotImplementedException();
    }
}