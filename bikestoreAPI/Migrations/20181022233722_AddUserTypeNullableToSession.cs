using Microsoft.EntityFrameworkCore.Migrations;

namespace bikestoreAPI.Migrations
{
    public partial class AddUserTypeNullableToSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserSessionType",
                table: "Session",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserSessionType",
                table: "Session",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
