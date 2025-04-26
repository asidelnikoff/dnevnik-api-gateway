using Dnevnik.ApiGateway.Services.Users.Dto;
using Dnevnik.ApiGateway.Services.Users.Models;

using User = Dnevnik.ApiGateway.Services.Users.Models.User;

namespace Dnevnik.ApiGateway.Services.Users;

public interface IUsersApiService
{
    Task<AuthResponse> PostAuth(AuthRequest request);
    Task<AuthResponse> PostRefresh(RefreshRequest request);
    Task PostLogout();
    Task<User> CreateUserAsync(CreateUser info);
    Task<User> GetUserInfoAsync(Guid id);
    Task<User> UpdateUserInfoAsync(Guid id, CreateUser info);
    Task DeleteUserAsync(Guid id);
    Task<User[]> GetUsersList(FilterRequest request);
}