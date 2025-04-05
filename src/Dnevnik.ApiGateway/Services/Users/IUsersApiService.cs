using Dnevnik.ApiGateway.Services.Users.Models;

namespace Dnevnik.ApiGateway.Services.Users;

public interface IUsersApiService
{
    // todo auth methods
    
    User CreateUser(CreateUser info);
    User GetUserInfo(Guid id);
    User UpdateUserInfo(Guid id, CreateUser info);
    void DeleteUser(Guid id);
    User[] GetUsersList(/*todo filter parameters*/);
}