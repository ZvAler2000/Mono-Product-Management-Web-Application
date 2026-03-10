using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Handheld electronic device used for communication.", "Smartphone" },
                    { 2, "Output device that displays visual information sent from the computer.", "Monitor" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsActive", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 3, 3, 18, 24, 19, 188, DateTimeKind.Utc).AddTicks(769), true, "iPhone 12", 899m, 100 },
                    { 2, 2, new DateTime(2026, 3, 3, 18, 24, 19, 188, DateTimeKind.Utc).AddTicks(1038), true, "Samsung Odyssey OLED G9", 1300m, 100 },
                    { 3, 1, new DateTime(2026, 3, 3, 18, 24, 19, 188, DateTimeKind.Utc).AddTicks(1040), true, "Samsung Galaxy S25", 799m, 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
