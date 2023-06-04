using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SqlServer.Migrations
{
    public partial class File_AltFileUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AltFileSize",
                table: "FileUpload",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AltFileUrl",
                table: "FileUpload",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltFileSize",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "AltFileUrl",
                table: "FileUpload");
        }
    }
}
