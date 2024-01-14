using AFC.Base.Entity;
using AFC.Base.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFC.Data.Entity;

[Table("ExpenseRequest", Schema = "dbo")]
public class ExpenseRequest : BaseEntity
{
    public int FieldStaffId { get; set; }
    public virtual FieldStaff FieldStaff { get; set; }
    public int PaymentCategoryId { get; set; }
    public virtual PaymentCategory PaymentCategory { get; set; }
    public decimal Amount { get; set; }
    public string PaymentLocation { get; set; }
    public string DocumentPath { get; set; }
    public ExpenseStatus ExpenseStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

public class ExpenseRequestConfiguration : IEntityTypeConfiguration<ExpenseRequest>
{
    public void Configure(EntityTypeBuilder<ExpenseRequest> builder)
    {
        builder.Property(x => x.CreateBy).IsRequired(true);
        builder.Property(x => x.CreateAt).IsRequired(true);
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.ModifiedAt).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.FieldStaffId).IsRequired(true);
        builder.Property(x => x.PaymentCategoryId).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18, 4);
        builder.Property(x => x.PaymentLocation).IsRequired(true).HasMaxLength(512);
        builder.Property(x => x.DocumentPath).IsRequired(true).HasMaxLength(512);
        builder.Property(x => x.ExpenseStatus).IsRequired(true);
        builder.Property(x => x.PaymentStatus).IsRequired(true);

        builder.HasIndex(x => x.FieldStaffId);
        builder.HasIndex(x => x.PaymentCategoryId);
    }
}
