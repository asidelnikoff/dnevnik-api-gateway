using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Journal.Models;

namespace Dnevnik.ApiGateway.Services.Journal;

public class JournalApiService(IHttpService httpService) : BaseApiService, IJournalApiService
{
    public Task<UserMark[]> CreateMarks(CreateMarkRequest[] marks)
    {
        throw new NotImplementedException();
    }

    public Task<UserMark[]> UpdateMarks(UserMark[] marks)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMarks(UserMark[] marks)
    {
        throw new NotImplementedException();
    }

    public Task<UserMark[]> GetMarks(FiltersRequest filters)
    {
        throw new NotImplementedException();
    }
}