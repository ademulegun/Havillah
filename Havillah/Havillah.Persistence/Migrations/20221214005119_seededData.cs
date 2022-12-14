using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Havillah.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class seededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d"), "3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("363b37a0-c306-4472-a405-4b576334cca0"), 0, "be2dd2c2-2352-4bad-8c36-16ec5cae36d3", "femi.ibitolu@gmail.com", true, "Babafemi", "Ibitolu", false, null, "Oluwaseyi", null, "FEMI.IBITOLU@GMAIL.COM", "AQAAAAEAACcQAAAAEKAI/Rh3HxN3op7KMvD3X93gdNB1FGAXVtBWoBQ96lWaPZAMQuq8v+wBPx/Kg4zKPA==", null, false, null, false, "femi.ibitolu@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d"), new Guid("363b37a0-c306-4472-a405-4b576334cca0") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d"), new Guid("363b37a0-c306-4472-a405-4b576334cca0") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("363b37a0-c306-4472-a405-4b576334cca0"));
        }
    }
}
