using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AFC.Data;

public class AfcDbContext : DbContext
{
    public AfcDbContext(DbContextOptions<AfcDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
