using AFC.Base.Entity;
using Microsoft.AspNetCore.Http;

namespace AFC.Business.Helpers;

public static class BaseEntitySetPropertyExtension
{
    /// <summary>
    /// BaseEntity'de bulunan Prop'ları nesne oluşurken set'ler.
    /// </summary>
    public static void SetCreatedProperties(this BaseEntity entity, IHttpContextAccessor httpContextAccessor)
    {
        int userId = GetUserIdFromClaims.GetUserId(httpContextAccessor);

        entity.CreateBy = userId;
        entity.CreateAt = DateTime.Now;
        entity.IsActive = true;
    }

    /// <summary>
    /// BaseEntity'de bulunan Prop'ları nesne güncellenirken set'ler.
    /// </summary>
    public static void SetModifiedProperties(this BaseEntity entity, IHttpContextAccessor httpContextAccessor)
    {
        int userId = GetUserIdFromClaims.GetUserId(httpContextAccessor);

        entity.ModifiedBy = userId;
        entity.ModifiedAt = DateTime.Now;
        entity.IsActive = true;
    }
}
