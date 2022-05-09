using Microsoft.EntityFrameworkCore.Migrations;

namespace Maktabty.Migrations
{
    public partial class addbookfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "book",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "book",
                table: "Books");
        }
    }
}
