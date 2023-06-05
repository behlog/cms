using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SQLite.Migrations
{
    public partial class UserIdentityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Website",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Website",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerUserName",
                table: "Website",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwneruserDisplayName",
                table: "Website",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Tag",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Tag",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByIp",
                table: "Tag",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Tag",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserId",
                table: "Tag",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Tag",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "FileUpload",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "FileUpload",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "FileUpload",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "FileUpload",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "ContentCategory",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "ContentCategory",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "ContentCategory",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "ContentCategory",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Content",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Content",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Content",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Content",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Component",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Component",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Component",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Component",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserDisplayName",
                table: "Comment",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Comment",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserDisplayName",
                table: "Comment",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedByUserName",
                table: "Comment",
                type: "TEXT",
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
        }
    }
}
