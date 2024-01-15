using AFC.Base.Entity;
using AFC.Base.Enums;
using AFC.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFC.Data.Entity;

[Table("User", Schema = "dbo")]
public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public int Status { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        BaseEntityConfigurationHelper.ConfigureBaseEntity(builder);

        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(256);
        builder.Property(x => x.Password).IsRequired(true).HasMaxLength(256);
        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(128);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(128);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(256);
        builder.Property(x => x.Role).IsRequired(true);
        builder.Property(x => x.LastActivityDate).IsRequired(true);
        builder.Property(x => x.PasswordRetryCount).IsRequired(true).HasDefaultValue(0);
        builder.Property(x => x.Status).IsRequired(true).HasDefaultValue(1);

        builder.HasIndex(x => x.UserName).IsUnique(true);
    }
}
