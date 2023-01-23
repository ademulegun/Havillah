using Havillah.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var userId = Guid.Parse("363b37a0-c306-4472-a405-4b576334cca0");
        var appUser = new ApplicationUser { 
            Id = userId,
            Email = "femi.ibitolu@gmail.com",
            EmailConfirmed = true, 
            FirstName = "Babafemi",
            MiddleName = "Oluwaseyi",
            LastName = "Ibitolu",
            UserName = "femi.ibitolu@gmail.com",
            NormalizedUserName = "femi.ibitolu@gmail.com".ToUpper(),
            PhoneNumber = "08122310370",
            Address = "No 1 Jango steet, wild wild west, Texas"
        };
        //set user password
        PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "Password@123");
        builder.Property(x => x.Address).HasColumnType("nvarchar(300)").IsRequired(true);
        builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar(300)").IsRequired(true);
        builder.HasData(appUser);
    }
}