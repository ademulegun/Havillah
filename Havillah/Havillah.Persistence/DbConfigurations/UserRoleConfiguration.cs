using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class UserRoleConfiguration: IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        var userId = Guid.Parse("363b37a0-c306-4472-a405-4b576334cca0");
        var roleId = Guid.Parse("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d");
        builder.HasData(new IdentityUserRole<Guid> { 
            RoleId = roleId, 
            UserId = userId 
        });
    }
}