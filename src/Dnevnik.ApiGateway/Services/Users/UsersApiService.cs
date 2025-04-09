using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Users.Models;

using User = Dnevnik.ApiGateway.Services.Users.Models.User;

namespace Dnevnik.ApiGateway.Services.Users;

public class UsersApiService(IHttpService httpService) : BaseApiService, IUsersApiService
{
    private const string UsersRoute = "users";

    public async Task<User> CreateUserAsync(CreateUser info)
    {
        var response = await httpService.PostAsync(new HttpPostRequest
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

    public async Task<Teacher> GetTeacherInfo(Guid id)
    {
        var response = await httpService.GetAsync(new BaseHttpRequest { Route = $"{UsersRoute}/{id}" });

        return JsonDeserialize<Teacher>(response);
    }

    public async Task<User> UpdateUserInfo(Guid id, CreateUser info)
    {
        var response = await httpService.PutAsync(new HttpPostRequest
        {
            Route = $"{UsersRoute}/{id}",
            Body = JsonSerialize(info)
        });

        return JsonDeserialize<User>(response);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await httpService.DeleteAsync(new BaseHttpRequest { Route = $"{UsersRoute}/{id}" });
    }

    public User[] GetUsersList()
    {
        throw new NotImplementedException();
    }
}