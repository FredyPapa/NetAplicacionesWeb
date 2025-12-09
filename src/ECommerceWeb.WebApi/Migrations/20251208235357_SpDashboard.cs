using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceWeb.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SpDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE uspDashboard
                AS
                BEGIN
	                SELECT 
			                SUM(V.Total) AS TotalVenta,
			                COUNT(V.Id) AS CantidadVentas,
			                (SELECT COUNT(C.Id) FROM Cliente C) CantidadClientes,
			                (SELECT COUNT(P.Id) FROM Productos P) CantidadProductos
		                FROM Venta v
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE uspDashboard");
        }
    }
}
