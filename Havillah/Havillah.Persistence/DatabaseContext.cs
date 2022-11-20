using Havillah.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Havillah.Persistence;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> context): base(context) { }
    
    public DbSet<Product> Product { get; set; }
}