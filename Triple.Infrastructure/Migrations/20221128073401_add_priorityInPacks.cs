using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triple.Infrastructure.Migrations
{
    public partial class add_priorityInPacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPriority",
                table: "Packs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPriority",
                table: "Packs");
        }
    }
}
