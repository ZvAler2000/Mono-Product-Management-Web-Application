using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class SeedData4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 3, "Street/sports footwear.", "Sneakers" },
                    { 4, "Cloth garment for upper body worn by both men and women.", "Shirt" },
                    { 5, "Mixture of fragrant oils and aromas that give a pleasant smell.", "Perfume" },
                    { 6, "Written work created by one or more authors.", "Book" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsActive", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 4, 2, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "HP E24 G4", 99m, 10 },
                    { 5, 3, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Air Jordan 1s", 299m, 50 },
                    { 6, 3, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nike Air Force 1s", 120m, 200 },
                    { 7, 4, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Comme de Garcons Graphic-print sweatshirt", 77m, 20 },
                    { 8, 4, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Zara relaxed fit flowing brown shirt", 35m, 250 },
                    { 9, 5, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Calvin Klein Obsession 30ml", 20m, 90 },
                    { 10, 5, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Chanel no. 5 eau de parfum 100ml", 185m, 70 },
                    { 11, 6, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Blood Meridian by Cormack McCarthy", 15m, 500 },
                    { 12, 6, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Darth Plagueis by James Luceno", 20m, 450 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
