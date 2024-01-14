using AFC.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFC.Data.Entity;

[Table("FieldStaff", Schema = "dbo")]
public class FieldStaff : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string IBAN { get; set; }
    public string UserName { get; set; }

    public virtual List<ExpenseRequest> ExpenseRequests { get; set; }
}

public class FieldStaffConfiguration : IEntityTypeConfiguration<FieldStaff>
{
    public void Configure(EntityTypeBuilder<FieldStaff> builder)
    {
        builder.Property(x => x.CreateBy).IsRequired(true);
        builder.Property(x => x.CreateAt).IsRequired(true);
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.ModifiedAt).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(26);
        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(128);

        builder.HasIndex(x => x.UserId).IsUnique(true);

        builder.HasMany(x => x.ExpenseRequests)
            .WithOne(x => x.FieldStaff)
            .HasForeignKey(x => x.FieldStaffId)
            .IsRequired(true);
    }
}
