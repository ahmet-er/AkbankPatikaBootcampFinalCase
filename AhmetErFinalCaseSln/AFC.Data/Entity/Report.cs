using AFC.Base.Entity;
using AFC.Base.Enums;
using AFC.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFC.Data.Entity;

[Table("Report", Schema = "dbo")]
public class Report : BaseEntity
{
    public string Name { get; set; }
    public ReportType ReportType { get; set; }
    public ReportPeriod ReportPeriod { get; set; }
}

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        BaseEntityConfigurationHelper.ConfigureBaseEntity(builder);

        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(128);
        builder.Property(x => x.ReportType).IsRequired(true);
        builder.Property(x => x.ReportPeriod).IsRequired(true);
        
    }
}
