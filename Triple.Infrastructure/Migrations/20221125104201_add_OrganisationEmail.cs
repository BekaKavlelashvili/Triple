using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triple.Infrastructure.Migrations
{
    public partial class add_OrganisationEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Organisation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Organisation");
        }
    }
}
