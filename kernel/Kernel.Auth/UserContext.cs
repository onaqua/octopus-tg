using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Kernel.Auth;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid Id => Guid.TryParse(httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value, out var id) ? 
        id : throw new InvalidOperationException("User is not authenticated");

    public string Name => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? 
        throw new InvalidOperationException("User is not authenticated");

    public string Email => httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value ??
        throw new InvalidOperationException("User is not authenticated");

    public IReadOnlyCollection<string> Roles => httpContextAccessor.HttpContext?.User?.Claims?
        .Where(x => x.Type.Equals(ClaimTypes.Role))?
        .Select(x => x.Value)?
        .ToHashSet() ?? [];

    public string GetAuthorizationCookie()
    {
        throw new NotImplementedException();
    }

    public string SetAuthorizationCookie(string value)
    {
        throw new NotImplementedException();
    }
}