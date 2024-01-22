using AFC.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AFC.Data.Helpers;

/// <summary>
/// BaseEntity'den gelen propertylerin configurasyonlarını yapar.
/// </summary>
public static class BaseEntityConfigurationExtension
{
    public static void ConfigureBaseEntity<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
    {
        builder.Property(x => x.CreateBy).IsRequired(true);
        builder.Property(x => x.CreateAt).IsRequired(true);
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.ModifiedAt).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
    }
}
