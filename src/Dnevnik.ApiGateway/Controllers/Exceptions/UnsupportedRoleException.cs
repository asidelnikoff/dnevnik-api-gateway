using Dnevnik.ApiGateway.Controllers.Dto;

namespace Dnevnik.ApiGateway.Controllers.Exceptions;

public class UnsupportedRoleException(Role role) : Exception($"Role {role} is unsupported in this context");