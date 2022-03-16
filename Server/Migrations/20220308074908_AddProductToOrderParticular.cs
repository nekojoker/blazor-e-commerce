using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEC.Server.Migrations
{
    public partial class AddProductToOrderParticular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderParticulars_ProductId",
                table: "OrderParticulars",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderParticulars_Products_ProductId",
                table: "OrderParticulars",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderParticulars_Products_ProductId",
                table: "OrderParticulars");

            migrationBuilder.DropIndex(
                name: "IX_OrderParticulars_ProductId",
                table: "OrderParticulars");
        }
    }
}
