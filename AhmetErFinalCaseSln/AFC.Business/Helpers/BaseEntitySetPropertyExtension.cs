using AFC.Base.Entity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AFC.Business.Helpers;

public static class BaseEntitySetPropertyExtension
{
    public static void SetCreatedProperties(this BaseEntity entity, IHttpContextAccessor httpContextAccessor)
    {
        int userId = GetUserIdFromClaims(httpContextAccessor);

        entity.CreateBy = userId;
        entity.CreateAt = DateTime.Now;
        entity.IsActive = true;
    }

    public static void SetModifiedProperties(this BaseEntity entity, IHttpContextAccessor httpContextAccessor)
    {
        int userId = GetUserIdFromClaims(httpContextAccessor);

        entity.ModifiedBy = userId;
        entity.ModifiedAt = DateTime.Now;
        entity.IsActive = true;
    }

    private static int GetUserIdFromClaims(IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst("Id");

        if (userIdClaim is not null && int.TryParse(userIdClaim.Value, out var userId))
            return userId;
        return 0;
    }
}
