using System.Reflection;
using Havillah.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Havillah.Persistence;

public class DatabaseContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> context): base(context) { }
    
    public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var userId = Guid.Parse("363b37a0-c306-4472-a405-4b576334cca0");
        var roleId = Guid.Parse("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d");
        
        //seed admin role
        builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid> { 
            Name = "SuperAdmin", 
            NormalizedName = "SUPERADMIN", 
            Id = roleId,
            ConcurrencyStamp = roleId.ToString()
        });
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var appUser = new ApplicationUser { 
            Id = userId,
            Email = "femi.ibitolu@gmail.com",
            EmailConfirmed = true, 
            FirstName = "Babafemi",
            MiddleName = "Oluwaseyi",
            LastName = "Ibitolu",
            UserName = "femi.ibitolu@gmail.com",
            NormalizedUserName = "femi.ibitolu@gmail.com".ToUpper()
        };
        //set user password
        PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "Password@123");
        builder.Entity<ApplicationUser>().HasData(appUser);
        //set user role to admin
        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> { 
            RoleId = roleId, 
            UserId = userId 
        });
        base.OnModelCreating(builder);
    }
}