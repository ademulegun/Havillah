using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        var roleId = Guid.Parse("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d");
        builder.HasData(new IdentityRole<Guid> { 
                Name = "SuperAdmin", 
                NormalizedName = "SUPERADMIN", 
                Id = roleId,
                ConcurrencyStamp = roleId.ToString()
            },
            new IdentityRole<Guid> { 
                Name = "Admin", 
                NormalizedName = "ADMIN",
                Id = Guid.Parse("ae215c6c-2f89-4646-a1cc-e3c1287bd6e4")
            },
            new IdentityRole<Guid> { 
                Name = "SalesPerson", 
                NormalizedName = "SALESPERSON",
                Id = Guid.Parse("ec2bfe1e-0fa4-4900-a312-1848f542b61a")
            });
    }
}