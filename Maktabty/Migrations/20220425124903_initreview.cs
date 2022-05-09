using Microsoft.EntityFrameworkCore.Migrations;

namespace Maktabty.Migrations
{
    public partial class initreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalRate",
                table: "Books",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalRate",
                table: "Books");
        }
    }
}
