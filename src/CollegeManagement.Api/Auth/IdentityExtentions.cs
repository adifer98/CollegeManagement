using System.Security.Claims;

namespace CollegeManagement.Api.Auth;

public static class IdentityExtentions
{
    public static Guid? GetUserId(this HttpContext context)
    {
        var userId = context.User.Claims.SingleOrDefault(claim => claim.Type == "UserId");
        
        if (Guid.TryParse(userId?.Value, out var parsedId))
        {
            return parsedId;
        }

        return null;
    }
}