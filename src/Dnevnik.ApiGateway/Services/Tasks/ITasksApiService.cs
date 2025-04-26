using Dnevnik.ApiGateway.Services.Tasks.Dto;
using Dnevnik.ApiGateway.Services.Tasks.Dto.Requests;
using Dnevnik.ApiGateway.Services.Tasks.Models;

using Task = Dnevnik.ApiGateway.Services.Tasks.Models.Task;

namespace Dnevnik.ApiGateway.Services.Tasks;

public interface ITasksApiService
{
    Task<Task> CreateTask(CreateTask task);
    Task<Task> GetTask(Guid id);
    Task<Task?> GetTaskOrDefault(Guid id, string className);
    Task<Task> UpdateTask(UpdateTaskRequest request);
    System.Threading.Tasks.Task DeleteTask(Guid id);
}