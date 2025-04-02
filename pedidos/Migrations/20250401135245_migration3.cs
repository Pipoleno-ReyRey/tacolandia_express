using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pedidos.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dishesOrded",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "dishesOrders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false),
                    dishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dishesOrders", x => new { x.dishId, x.orderId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dishesOrders");

            migrationBuilder.AddColumn<string>(
                name: "dishesOrded",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
