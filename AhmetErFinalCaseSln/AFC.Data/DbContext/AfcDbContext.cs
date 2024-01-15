using AFC.Base.Entity;
using AFC.Base.Enums;
using AFC.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AFC.Data;

public class AfcDbContext : DbContext
{
    public AfcDbContext(DbContextOptions<AfcDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    CreateAt = DateTime.Now,
                    CreateBy = 1,
                    UserName = "admin1",
                    LastActivityDate = DateTime.Now,
                    FirstName = "Admin",
                    LastName = "Admin1",
                    Email = "admin1@gmail.com",
                    Password = "Admin1!",
                    Role = Role.Admin
                },
                new User
                {
                    Id = 2,
                    CreateAt = DateTime.Now,
                    CreateBy = 1,
                    UserName = "admin2",
                    LastActivityDate = DateTime.Now,
                    FirstName = "Admin",
                    LastName = "Admin2",
                    Email = "admin2@gmail.com",
                    Password = "Admin2!",
                    Role = Role.Admin
                }
            );
        base.OnModelCreating(modelBuilder);
    }
}
