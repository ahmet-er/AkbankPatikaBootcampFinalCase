using AFC.Base.Entity;
using AFC.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFC.Data.Entity;

[Table("ExpenseDocument", Schema = "dbo")]
public class ExpenseDocument : BaseEntity 
{
    public int ExpenseRequestId { get; set; }
    public ExpenseRequest ExpenseRequest { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
}

public class ExpenseDocumentConfiguration : IEntityTypeConfiguration<ExpenseDocument>
{
    public void Configure(EntityTypeBuilder<ExpenseDocument> builder)
    {
        BaseEntityConfigurationHelper.ConfigureBaseEntity(builder);

        builder.Property(x => x.ExpenseRequestId).IsRequired(true);
        builder.Property(x => x.FilePath).IsRequired(true).HasMaxLength(512);
        builder.Property(x => x.FileType).IsRequired(true).HasMaxLength(32);
        builder.Property(x => x.FileName).IsRequired(true).HasMaxLength(256);

        builder.HasOne(x => x.ExpenseRequest)
            .WithMany(x => x.ExpenseDocuments)
            .HasForeignKey(x => x.ExpenseRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
