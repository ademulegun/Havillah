using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Havillah.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class addedcolorssizesandbrandnametoproductandcategoryid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_ProductId",
                table: "Stocks",
                newName: "IX_Stocks_ProductId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdded",
                table: "Product",
                type: "Datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Product",
                type: "nvarchar(90)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Colours",
                table: "Product",
                type: "nvarchar(300)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sizes",
                table: "Product",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "Datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ae215c6c-2f89-4646-a1cc-e3c1287bd6e4"),
                column: "ConcurrencyStamp",
                value: "edd5d345-c128-4846-b6ea-d4b4a2d26e71");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec2bfe1e-0fa4-4900-a312-1848f542b61a"),
                column: "ConcurrencyStamp",
                value: "7fdb61a0-98a2-4dc5-a9c6-030d9b3cbfd5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("363b37a0-c306-4472-a405-4b576334cca0"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a84a7264-f360-4c6d-90ef-41539ec4225a", "AQAAAAEAACcQAAAAEPjBNELb+T6c0VRVZvh3H5oArMRF0zM3ptyOf4E4wJSh4VurW0c1Mu2vMyS2IUAMkw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Product_ProductId",
                table: "Stocks",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Product_ProductId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Colours",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Sizes",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ProductId",
                table: "Stock",
                newName: "IX_Stock_ProductId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdded",
                table: "Product",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
