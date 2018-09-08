using Microsoft.EntityFrameworkCore.Migrations;

namespace bikestoreAPI.Migrations
{
    public partial class UpdateProduct_ShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Shipping",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Product");
        }
    }
}
