namespace Dnevnik.ApiGateway.Controllers.Dto;

public class Pagination
{
    public int Page { get; init; } = 1;
    public int Limit { get; init; } = 20;
    
    public int Offset => (Page - 1) * Limit;
}