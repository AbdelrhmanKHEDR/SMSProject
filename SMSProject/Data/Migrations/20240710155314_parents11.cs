using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSProject.Data.Migrations
{
    public partial class parents11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parents1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parents2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parents1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Parents2",
                table: "AspNetUsers");
        }
    }
}
