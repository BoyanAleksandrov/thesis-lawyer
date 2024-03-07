using System.Security.Claims;

namespace thesis_lawyer.Extensions;

public static class ClaimsExtension
{
    public static string GetFirstName(this ClaimsPrincipal principal)
    {
        var firstName = principal.Claims.FirstOrDefault(x => x.Type == "FirstName");
        return firstName?.Value;
    }
}