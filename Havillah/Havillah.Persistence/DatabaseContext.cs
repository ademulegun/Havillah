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
    public DbSet<ProductCategory> Categories { get; set; }
    public DbSet<Stock> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}