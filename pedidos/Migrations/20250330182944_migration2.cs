using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pedidos.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredients_Dish_DishId",
                table: "ingredients");

            migrationBuilder.DropIndex(
                name: "IX_ingredients_DishId",
                table: "ingredients");

            migrationBuilder.DropColumn(
                name: "DishId",
                table: "ingredients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DishId",
                table: "ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_DishId",
                table: "ingredients",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredients_Dish_DishId",
                table: "ingredients",
                column: "DishId",
                principalTable: "Dish",
                principalColumn: "Id");
        }
    }
}
