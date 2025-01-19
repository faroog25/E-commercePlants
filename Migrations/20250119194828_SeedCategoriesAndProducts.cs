using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_commercePlants.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoriesAndProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "Shirts", "shirts" },
                    { 2, "Fruit", "fruit" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { 1, 2, "Juicy apples", "apple1.jpg", "Apples", 1.50m, "apples" },
                    { 2, 2, "Juicy grapefruit", "grapefruit1.jpg", "Grapefruit", 2m, "grapefruit" },
                    { 3, 2, "Fresh grapes", "grapes1.jpg", "Grapes", 1.80m, "grapes" },
                    { 4, 2, "Fresh oranges", "orange1.jpg", "Oranges", 1.50m, "oranges" },
                    { 5, 1, "Nice blue t-shirt", "blue1.jpg", "Blue shirt", 7.99m, "blue-shirt" },
                    { 6, 1, "Nice red t-shirt", "red1.jpg", "Red shirt", 8.99m, "red-shirt" },
                    { 7, 1, "Nice green t-shirt", "green1.png", "Green shirt", 9.99m, "green-shirt" },
                    { 8, 1, "Nice pink t-shirt", "pink1.png", "Pink shirt", 10.99m, "pink-shirt" }
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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
