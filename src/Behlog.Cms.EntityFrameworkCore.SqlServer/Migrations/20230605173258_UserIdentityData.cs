using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SqlServer.Migrations
{
    public partial class UserIdentityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedByUserId",
                table: "Website",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedByIp",
                table: "Website",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Website",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Website",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerUserName",
                table: "Website",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwneruserDisplayName",
                table: "Website",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Tag",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Tag",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByIp",
                table: "Tag",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Tag",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserId",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Tag",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "FileUpload",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "FileUpload",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "FileUpload",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "FileUpload",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedByUserId",
                table: "ContentCategory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "ContentCategory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "ContentCategory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "ContentCategory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "ContentCategory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "ContentCategory",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Content",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Content",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Content",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Content",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Component",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Component",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Component",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Component",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Comment",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Comment",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Comment",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Comment",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserDisplayName",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserName",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "OwnerUserName",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "OwneruserDisplayName",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "CreatedByUserDisplayName",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByIp",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserDisplayName",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserName",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedByUserDisplayName",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserDisplayName",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserName",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "CreatedByUserDisplayName",
                table: "ContentCategory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "ContentCategory");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserDisplayName",
                table: "ContentCategory");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserName",
                table: "ContentCategory");

            migrationBuilder.DropColumn(
                name: "CreatedByUserDisplayName",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserDisplayName",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserName",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "CreatedByUserDisplayName",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserDisplayName",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserName",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "CreatedByUserDisplayName",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserDisplayName",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserName",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedByUserId",
                table: "Website",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedByIp",
                table: "Website",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedByUserId",
                table: "ContentCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "ContentCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
