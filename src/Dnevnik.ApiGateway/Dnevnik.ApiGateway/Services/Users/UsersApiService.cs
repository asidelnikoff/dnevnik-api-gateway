using Dnevnik.ApiGateway.Services.HttpService;

namespace Dnevnik.ApiGateway.Services.Users;

public class UsersApiService(IHttpService httpService) : BaseApiService, IUsersApiService
{
    
}