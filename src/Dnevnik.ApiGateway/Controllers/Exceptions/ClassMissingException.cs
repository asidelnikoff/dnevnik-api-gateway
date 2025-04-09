namespace Dnevnik.ApiGateway.Controllers.Exceptions;

public class ClassMissingException(string name) : Exception($"Class is missing for user {name}");