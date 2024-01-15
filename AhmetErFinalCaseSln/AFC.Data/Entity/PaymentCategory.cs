using AFC.Base.Entity;
using AFC.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFC.Data.Entity;

[Table("PaymentCategory", Schema = "dbo")]
public class PaymentCategory : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual List<ExpenseRequest> ExpenseRequests { get; set; }
}

public class PaymentCategoryConfiguration : IEntityTypeConfiguration<PaymentCategory>
{
    public void Configure(EntityTypeBuilder<PaymentCategory> builder)
    {
        BaseEntityConfigurationHelper.ConfigureBaseEntity(builder);

        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(128);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(512);

        builder.HasMany(x => x.ExpenseRequests)
            .WithOne(x => x.PaymentCategory)
            .HasForeignKey(x => x.PaymentCategoryId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
