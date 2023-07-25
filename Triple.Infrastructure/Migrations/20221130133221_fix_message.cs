using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triple.Infrastructure.Migrations
{
    public partial class fix_message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lasname",
                table: "ChatMessages",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Firsname",
                table: "ChatMessages",
                newName: "Firstname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "ChatMessages",
                newName: "Lasname");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "ChatMessages",
                newName: "Firsname");
        }
    }
}
