using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Users.Models;

using User = Dnevnik.ApiGateway.Services.Users.Models.User;

namespace Dnevnik.ApiGateway.Services.Users;

public class UsersApiService(IHttpService httpService) : BaseApiService, IUsersApiService
{
    public User CreateUser(CreateUser info)
    {
        throw new NotImplementedException();
    }

    public User GetUserInfo(Guid id)
    {
        throw new NotImplementedException();
    }

    public Teacher GetTeacherInfo(Guid id)
    {
        throw new NotImplementedException();
    }

    public User UpdateUserInfo(Guid id, CreateUser info)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public User[] GetUsersList()
    {
        throw new NotImplementedException();
    }
}