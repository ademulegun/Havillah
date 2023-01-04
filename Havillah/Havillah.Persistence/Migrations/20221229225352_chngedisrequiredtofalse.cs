using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Havillah.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class chngedisrequiredtofalse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductImageUrl",
                table: "Product",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Product",
                type: "nvarchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ae215c6c-2f89-4646-a1cc-e3c1287bd6e4"),
                column: "ConcurrencyStamp",
                value: "ea9256ad-beb0-48dd-9481-da7e6ae80661");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec2bfe1e-0fa4-4900-a312-1848f542b61a"),
                column: "ConcurrencyStamp",
                value: "82949f87-aa58-4288-af72-6da937b10065");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("363b37a0-c306-4472-a405-4b576334cca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "18231cf2-f40d-4dee-9461-9c17b77ecfe6", "AQAAAAEAACcQAAAAEIaT7dn3JDhId7tHsxjcKBmtQJQCX++Oh2u4PHWkl6JDI63/4XV6OrWCecVce4uixg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductImageUrl",
                table: "Product",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Product",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ae215c6c-2f89-4646-a1cc-e3c1287bd6e4"),
                column: "ConcurrencyStamp",
                value: "1fbdb3c3-56f6-475b-95df-a0df5cbd2c4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec2bfe1e-0fa4-4900-a312-1848f542b61a"),
                column: "ConcurrencyStamp",
                value: "fb5a128b-837a-419b-af44-7b8a5212191b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("363b37a0-c306-4472-a405-4b576334cca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ded8f3c4-cbf8-4bcb-83e4-7f885e943bed", "AQAAAAEAACcQAAAAEL+91P3536O1+hDf5Be/YjjOhdNYwHxjJUI5twX0S2s8fIvR0q6kgfsNt6Rugm0olA==" });
        }
    }
}
