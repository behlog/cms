using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Behlog.Cms.EntityFrameworkCore.SQLite.Migrations
{
    public partial class WebsiteTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentTypeTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContentTypeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LangId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagTitle = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    TagSlug = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "WebsiteTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WebsiteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LangId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContentTypeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagTitle = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    TagSlug = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteTag_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebsiteTag_ContentType_ContentTypeId",
                        column: x => x.ContentTypeId,
                        principalTable: "ContentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebsiteTag_Language_LangId",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebsiteTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebsiteTag_Website_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Website",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentTypeTag");

            migrationBuilder.DropTable(
                name: "WebsiteTag");
        }
    }
}
