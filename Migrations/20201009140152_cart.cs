using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLanchesMVC.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartPurchaseItens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LunchId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    CartPurchaseId = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartPurchaseItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartPurchaseItens_Lunches_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartPurchaseItens_LunchId",
                table: "CartPurchaseItens",
                column: "LunchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartPurchaseItens");
        }
    }
}
