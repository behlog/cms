using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SqlServer.Migrations
{
    public partial class FileUpload_FileUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteTag",
                table: "WebsiteTag");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteTag_TagId",
                table: "WebsiteTag");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "WebsiteTag",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "WebsiteTag",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContentTypeId",
                table: "WebsiteTag",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "FileUpload",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserDisplayName",
                table: "Content",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserName",
                table: "Content",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteTag",
                table: "WebsiteTag",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContentAuthor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAuthor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentAuthor_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AltTitle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BodyType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Body = table.Column<string>(type: "nTEXT", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserDisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentHistory_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentTypeTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagTitle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TagSlug = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypeTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentTypeTag_ContentType_ContentTypeId",
                        column: x => x.ContentTypeId,
                        principalTable: "ContentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentTypeTag_Language_LangId",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentTypeTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteTag_ContentId",
                table: "WebsiteTag",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteTag_ContentTypeId",
                table: "WebsiteTag",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteTag_LangId",
                table: "WebsiteTag",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteTag_TagId_WebsiteId_ContentId",
                table: "WebsiteTag",
                columns: new[] { "TagId", "WebsiteId", "ContentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteTag_WebsiteId",
                table: "WebsiteTag",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAuthor_ContentId",
                table: "ContentAuthor",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentHistory_ContentId",
                table: "ContentHistory",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypeTag_ContentTypeId",
                table: "ContentTypeTag",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypeTag_LangId",
                table: "ContentTypeTag",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypeTag_TagId_ContentTypeId",
                table: "ContentTypeTag",
                columns: new[] { "TagId", "ContentTypeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WebsiteTag_Content_ContentId",
                table: "WebsiteTag",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebsiteTag_ContentType_ContentTypeId",
                table: "WebsiteTag",
                column: "ContentTypeId",
                principalTable: "ContentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebsiteTag_Language_LangId",
                table: "WebsiteTag",
                column: "LangId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteTag_Content_ContentId",
                table: "WebsiteTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteTag_ContentType_ContentTypeId",
                table: "WebsiteTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WebsiteTag_Language_LangId",
                table: "WebsiteTag");

            migrationBuilder.DropTable(
                name: "ContentAuthor");

            migrationBuilder.DropTable(
                name: "ContentHistory");

            migrationBuilder.DropTable(
                name: "ContentTypeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteTag",
                table: "WebsiteTag");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteTag_ContentId",
                table: "WebsiteTag");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteTag_ContentTypeId",
                table: "WebsiteTag");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteTag_LangId",
                table: "WebsiteTag");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteTag_TagId_WebsiteId_ContentId",
                table: "WebsiteTag");

            migrationBuilder.DropIndex(
                name: "IX_WebsiteTag_WebsiteId",
                table: "WebsiteTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WebsiteTag");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "WebsiteTag");

            migrationBuilder.DropColumn(
                name: "ContentTypeId",
                table: "WebsiteTag");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "FileUpload");

            migrationBuilder.DropColumn(
                name: "AuthorUserDisplayName",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "AuthorUserName",
                table: "Content");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteTag",
                table: "WebsiteTag",
                columns: new[] { "WebsiteId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteTag_TagId",
                table: "WebsiteTag",
                column: "TagId");
        }
    }
}
