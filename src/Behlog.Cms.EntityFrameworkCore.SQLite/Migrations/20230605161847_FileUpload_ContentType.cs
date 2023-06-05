using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SQLite.Migrations
{
    public partial class FileUpload_ContentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileUpload",
                type: "TEXT",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileUpload");
        }
    }
}
