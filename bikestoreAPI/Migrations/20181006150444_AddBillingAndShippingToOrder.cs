using Microsoft.EntityFrameworkCore.Migrations;

namespace bikestoreAPI.Migrations
{
    public partial class AddBillingAndShippingToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Order",
                newName: "BillingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                table: "Order",
                newName: "IX_Order_BillingAddressId");

            migrationBuilder.AddColumn<int>(
                name: "ShippingAddressId",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_BillingAddressId",
                table: "Order",
                column: "BillingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_BillingAddressId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "BillingAddressId",
                table: "Order",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_BillingAddressId",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
