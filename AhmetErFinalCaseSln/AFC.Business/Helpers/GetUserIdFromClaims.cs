using Microsoft.AspNetCore.Http;

namespace AFC.Business.Helpers;

public static class GetUserIdFromClaims
{
    /// <summary>
    /// Claim'de bulunan User'in Id'sine çeker.
    /// </summary>
    public static int GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst("Id");

        if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out var userId))
            return userId;
        return 0;
    }
}
