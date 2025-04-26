using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Users.Dto;

using User = Dnevnik.ApiGateway.Services.Users.Models.User;

namespace Dnevnik.ApiGateway.Services.Users;

public class UsersApiService(IHttpService httpService) : BaseApiService, IUsersApiService
{
    private const string UsersRoute = "users";

    public Task<AuthResponse> PostAuth(AuthRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponse> PostRefresh(RefreshRequest request)
    {
        throw new NotImplementedException();
    }

    public Task PostLogout()
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateUserAsync(CreateUser info)
    {
        var response = await httpService.PostAsync(new HttpWithBodyRequest
        {
            Body = JsonSerialize(info),
            Route = UsersRoute
        });
        
        return JsonDeserialize<User>(response);
    }

    public async Task<User> GetUserInfoAsync(Guid id)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest { Route = $"{UsersRoute}/{id}" });

        return JsonDeserialize<User>(response);
    }

    public async Task<User> UpdateUserInfoAsync(Guid id, CreateUser info)
    {
        var response = await httpService.PutAsync(new HttpWithBodyRequest
        {
            Route = $"{UsersRoute}/{id}",
            Body = JsonSerialize(info)
        });

        return JsonDeserialize<User>(response);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await httpService.DeleteAsync(new HttpWithBodyRequest { Route = $"{UsersRoute}/{id}" });
    }

    public async Task<User[]> GetUsersList(FilterRequest request)
    {
        var response = await httpService.PostAsync(new HttpWithBodyRequest
        {
            Body = JsonSerialize(request),
            Route = UsersRoute
        });
        
        return JsonDeserialize<User[]>(response);
    }
}