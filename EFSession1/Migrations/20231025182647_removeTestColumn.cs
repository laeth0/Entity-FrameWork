using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFSession1.Migrations
{
    public partial class removeTestColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Test",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
