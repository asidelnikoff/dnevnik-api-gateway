using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.Users.Models;

using User = Dnevnik.ApiGateway.Services.Users.Models.User;

namespace Dnevnik.ApiGateway.Services.Users;

public interface IUsersApiService
{
    // todo auth methods
    
    Task<User> CreateUserAsync(CreateUser info);
    Task<User> GetUserInfoAsync(Guid id);
    Task<User> UpdateUserInfoAsync(Guid id, CreateUser info);
    Task DeleteUserAsync(Guid id);
    User[] GetUsersList(/*todo filter parameters*/);
}