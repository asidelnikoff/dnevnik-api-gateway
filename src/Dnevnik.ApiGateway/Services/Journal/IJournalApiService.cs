using Dnevnik.ApiGateway.Services.Journal.Models;

namespace Dnevnik.ApiGateway.Services.Journal;

public interface IJournalApiService
{
    Task<UserMark[]> CreateMarks(CreateMarkRequest[] marks);
    Task<UserMark[]> UpdateMarks(UserMark[] marks);
    Task DeleteMarks(UserMark[] marks);
    Task<UserMark[]> GetMarks(FiltersRequest filters);
}