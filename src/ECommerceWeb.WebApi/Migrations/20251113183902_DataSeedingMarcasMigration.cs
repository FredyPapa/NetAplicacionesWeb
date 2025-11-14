using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceWeb.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingMarcasMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 11, "Samsung 2" },
                    { 12, "Apple 2" },
                    { 13, "Xiaomi 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
