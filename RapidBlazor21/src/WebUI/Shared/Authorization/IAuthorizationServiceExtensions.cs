using Microsoft.AspNetCore.Authorization;
using RapidBlazor21.WebUI.Shared.Authorization;
using System.Security.Claims;

namespace RapidBlazor21.WebUI.Shared.Authorization;

public static class IAuthorizationServiceExtensions
{
    public static Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, Permissions permissions)
    {
        return service.AuthorizeAsync(user, PolicyNameHelper.GeneratePolicyNameFor(permissions));
    }
}
