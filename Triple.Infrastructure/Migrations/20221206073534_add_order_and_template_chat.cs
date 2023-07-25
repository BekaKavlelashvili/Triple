using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Triple.Infrastructure.Migrations
{
    public partial class add_order_and_template_chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemplateNote",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "TemplateNote",
                table: "Chats");
        }
    }
}
