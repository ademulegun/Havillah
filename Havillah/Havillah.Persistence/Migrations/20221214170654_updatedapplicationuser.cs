using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Havillah.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class updatedapplicationuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("ae215c6c-2f89-4646-a1cc-e3c1287bd6e4"), "bbe0cb85-3318-432b-9f38-39255d9ec7ed", "Admin", "ADMIN" },
                    { new Guid("ec2bfe1e-0fa4-4900-a312-1848f542b61a"), "b66b9c36-af9c-4872-900c-b6e86f18adcb", "SalesPerson", "SALESPERSON" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("363b37a0-c306-4472-a405-4b576334cca0"),
                columns: new[] { "Address", "ConcurrencyStamp", "PasswordHash", "PhoneNumber" },
                values: new object[] { "No 1 Jango steet, wild wild west, Texas", "9e298d0c-e6c1-448f-b670-e5234e568cee", "AQAAAAEAACcQAAAAEOx5mp9JBbQPJJGtnRs8wQg+iV+MnGadqfYPWPA5KMLfv7vkIVTRjGuRQPpoURA+Lw==", "08122310370" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ae215c6c-2f89-4646-a1cc-e3c1287bd6e4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec2bfe1e-0fa4-4900-a312-1848f542b61a"));

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("363b37a0-c306-4472-a405-4b576334cca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber" },
                values: new object[] { "be2dd2c2-2352-4bad-8c36-16ec5cae36d3", "AQAAAAEAACcQAAAAEKAI/Rh3HxN3op7KMvD3X93gdNB1FGAXVtBWoBQ96lWaPZAMQuq8v+wBPx/Kg4zKPA==", null });
        }
    }
}
