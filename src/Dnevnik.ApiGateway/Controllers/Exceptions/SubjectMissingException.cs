namespace Dnevnik.ApiGateway.Controllers.Exceptions;

public class SubjectMissingException(string name) : Exception($"Subject is missing for user {name}");