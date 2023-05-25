using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations.Product
{
    /// <inheritdoc />
    public partial class RelatedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryEntityId",
                table: "ProductItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductCategoryEntityId",
                table: "ProductItems",
                column: "ProductCategoryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_ProductCategories_ProductCategoryEntityId",
                table: "ProductItems",
                column: "ProductCategoryEntityId",
                principalTable: "ProductCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_ProductCategories_ProductCategoryEntityId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_ProductCategoryEntityId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "ProductCategoryEntityId",
                table: "ProductItems");
        }
    }
}
