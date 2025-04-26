using Dnevnik.ApiGateway.Controllers.Dto;
using Dnevnik.ApiGateway.Controllers.Dto.Responses;

namespace Dnevnik.ApiGateway.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<StudentInfoResponse> SortBy(this IEnumerable<StudentInfoResponse> students, StudentsSort sort)
    {
        return sort switch
        {
            StudentsSort.NameAsc => students.OrderBy(a => a.FullName),
            StudentsSort.NameDesc => students.OrderByDescending(a => a.FullName),
            StudentsSort.ClassAsc => students.OrderBy(a => a.Class),
            StudentsSort.ClassDesc => students.OrderByDescending(a => a.Class),
            _ => throw new ArgumentOutOfRangeException(nameof(sort), sort, null)
        };
    }
    
    public static IEnumerable<StuffInfoResponse> SortBy(this IEnumerable<StuffInfoResponse> stuff, StuffSort sort)
    {
        return sort switch
        {
            StuffSort.NameAsc => stuff.OrderBy(a => a.FullName),
            StuffSort.NameDesc => stuff.OrderByDescending(a => a.FullName),
            StuffSort.SubjectAsc => stuff.OrderBy(a => a.Subject),
            StuffSort.SubjectDesc => stuff.OrderByDescending(a => a.Subject),
            _ => throw new ArgumentOutOfRangeException(nameof(sort), sort, null)
        };
    }
}