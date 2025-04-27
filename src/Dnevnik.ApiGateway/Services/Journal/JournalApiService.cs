using Dnevnik.ApiGateway.Services.ApiService;
using Dnevnik.ApiGateway.Services.HttpService;
using Dnevnik.ApiGateway.Services.Journal.Models;

namespace Dnevnik.ApiGateway.Services.Journal;

public class JournalApiService(IHttpService httpService) : BaseApiService, IJournalApiService
{
    private const string Marks = "marks";
    
    public async Task<UserMark[]> CreateMarks(CreateMarkRequest[] marks)
    {
        var response = await httpService.PostAsync(new HttpWithBodyRequest
        {
            Route = Marks,
            Body = JsonSerialize(marks)
        });

        return JsonDeserialize<UserMark[]>(response);
    }

    public async Task<UserMark[]> UpdateMarks(UserMark[] marks)
    {
        var response = await httpService.PutAsync(new HttpWithBodyRequest
        {
            Route = Marks,
            Body = JsonSerialize(marks)
        });

        return JsonDeserialize<UserMark[]>(response);
    }

    public async Task DeleteMarks(UserMark[] marks)
    {
        await httpService.DeleteAsync(new HttpWithBodyRequest
        {
            Route = Marks,
            Body = JsonSerialize(marks)
        });
    }

    public async Task<UserMark[]> GetMarks(FiltersRequest filters)
    {
        var response = await httpService.GetAsync(new HttpWithBodyRequest
        {
            Route = $"{Marks}?{CreateFiltersQuery(filters)}"
        });

        return JsonDeserialize<UserMark[]>(response);
    }

    private string CreateFiltersQuery(FiltersRequest filter)
    {
        var result = "";
        if (filter.UserId is not null)
        {
            result += $"UserId={filter.UserId}&";
        }

        if (filter.LessonId is not null)
        {
            result += $"LessonId={filter.LessonId}&";
        }

        if (filter.Subject is not null)
        {
            result += $"Subject={filter.Subject}&";
        }

        if (filter.From is not null)
        {
            result += $"From={filter.From}&";
        }

        if (filter.To is not null)
        {
            result += $"To={filter.To}&";
        }

        return result.Trim('&');
    }
}