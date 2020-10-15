using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLanchesMVC.Migrations
{
    public partial class totalItemsOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalItemsOrder",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItemsOrder",
                table: "Orders");
        }
    }
}
