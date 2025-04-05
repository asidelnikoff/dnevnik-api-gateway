using Dnevnik.ApiGateway.Services.Tasks.Models;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Services.Tasks;

public interface ITasksApiService
{
    Task CreateTask(CreateTask task);
    Task GetTask(Guid id);
    Task UpdateTask(Task updatedTask);
    void DeleteTask(Guid id);
    
}