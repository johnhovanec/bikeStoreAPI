using Microsoft.EntityFrameworkCore.Migrations;

namespace bikestoreAPI.Migrations
{
    public partial class AddUserTypeToSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserSessionType",
                table: "Session",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserSessionType",
                table: "Session");
        }
    }
}
