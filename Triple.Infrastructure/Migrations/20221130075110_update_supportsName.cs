using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triple.Infrastructure.Migrations
{
    public partial class update_supportsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Supports",
                table: "Supports");

            migrationBuilder.RenameTable(
                name: "Supports",
                newName: "SupportUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportUsers",
                table: "SupportUsers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportUsers",
                table: "SupportUsers");

            migrationBuilder.RenameTable(
                name: "SupportUsers",
                newName: "Supports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supports",
                table: "Supports",
                column: "Id");
        }
    }
}
