using System.Security.Claims;

namespace SystemDesign.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Reads the current user's id from the standard NameIdentifier ("sub") claim.
    /// </summary>
    public static string? GetUserId(this ClaimsPrincipal user) =>
        user.FindFirstValue(ClaimTypes.NameIdentifier);
}
