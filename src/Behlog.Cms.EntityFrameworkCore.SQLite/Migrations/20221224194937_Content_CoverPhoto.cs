using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SQLite.Migrations
{
    public partial class Content_CoverPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentCategoryItem_ContentCategory_ContentId",
                table: "ContentCategoryItem");

            migrationBuilder.AddColumn<string>(
                name: "CoverPhoto",
                table: "Content",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentCategoryItem_CategoryId",
                table: "ContentCategoryItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentCategoryItem_ContentCategory_CategoryId",
                table: "ContentCategoryItem",
                column: "CategoryId",
                principalTable: "ContentCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentCategoryItem_ContentCategory_CategoryId",
                table: "ContentCategoryItem");

            migrationBuilder.DropIndex(
                name: "IX_ContentCategoryItem_CategoryId",
                table: "ContentCategoryItem");

            migrationBuilder.DropColumn(
                name: "CoverPhoto",
                table: "Content");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentCategoryItem_ContentCategory_ContentId",
                table: "ContentCategoryItem",
                column: "ContentId",
                principalTable: "ContentCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
