using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Services.Users.Models;

using User = Dnevnik.ApiGateway.Services.Users.Models.User;

namespace Dnevnik.ApiGateway.Services.Users;

public interface IUsersApiService
{
    // todo auth methods
    
    User CreateUser(CreateUser info);
    User GetUserInfo(Guid id);
    Teacher GetTeacherInfo(Guid id);
    User UpdateUserInfo(Guid id, CreateUser info);
    void DeleteUser(Guid id);
    User[] GetUsersList(/*todo filter parameters*/);
}