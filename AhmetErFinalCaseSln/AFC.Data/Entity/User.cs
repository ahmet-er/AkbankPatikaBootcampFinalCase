using AFC.Base.Entity;
using AFC.Base.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFC.Data.Entity;

[Table("User", Schema = "dbo")]
public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.CreateBy).IsRequired(true);
        builder.Property(x => x.CreateAt).IsRequired(true);
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.ModifiedAt).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(128);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(128);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(256);
        builder.Property(x => x.Password).IsRequired(true).HasMaxLength(256);
        builder.Property(x => x.Role).IsRequired(true);
    }
}
