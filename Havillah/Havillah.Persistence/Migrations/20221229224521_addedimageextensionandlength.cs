using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Havillah.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class addedimageextensionandlength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProductImage",
                table: "Product",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ProductImageExtension",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ProductImageLength",
                table: "Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductImageExtension",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductImageLength",
                table: "Product");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ae215c6c-2f89-4646-a1cc-e3c1287bd6e4"),
                column: "ConcurrencyStamp",
                value: "bbe0cb85-3318-432b-9f38-39255d9ec7ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec2bfe1e-0fa4-4900-a312-1848f542b61a"),
                column: "ConcurrencyStamp",
                value: "b66b9c36-af9c-4872-900c-b6e86f18adcb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("363b37a0-c306-4472-a405-4b576334cca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e298d0c-e6c1-448f-b670-e5234e568cee", "AQAAAAEAACcQAAAAEOx5mp9JBbQPJJGtnRs8wQg+iV+MnGadqfYPWPA5KMLfv7vkIVTRjGuRQPpoURA+Lw==" });
        }
    }
}
