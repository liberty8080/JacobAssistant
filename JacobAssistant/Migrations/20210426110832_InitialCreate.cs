using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JacobAssistant.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "config",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "config_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: true),
                    type_name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    comment = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "novel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    raw_id = table.Column<string>(type: "varchar(255)", nullable: true, comment: "来源网站的小说id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    novel_name = table.Column<string>(type: "varchar(255)", nullable: true, comment: "小说名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_source = table.Column<string>(type: "varchar(255)", nullable: true, comment: "来源网站")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    brief = table.Column<string>(type: "varchar(2000)", nullable: true, comment: "简介")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cover = table.Column<string>(type: "varchar(255)", nullable: true, comment: "封面")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    author = table.Column<string>(type: "varchar(20)", nullable: true, comment: "作者")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    raw_url = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_novel", x => x.id);
                },
                comment: "小说表");

            migrationBuilder.CreateTable(
                name: "novel_content",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    raw_content = table.Column<string>(type: "text", nullable: true, comment: "未经处理的原始数据")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "text", nullable: true, comment: "处理好的纯文字内容")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_novel_content", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "novel_chapter",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    novel_id = table.Column<int>(type: "int", nullable: true),
                    chapter_name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content_id = table.Column<int>(type: "int", nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_novel_chapter", x => x.id);
                    table.ForeignKey(
                        name: "novel_chapter_novel_content_id_fk",
                        column: x => x.content_id,
                        principalTable: "novel_content",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "novel_chapter_novel_id_fk",
                        column: x => x.novel_id,
                        principalTable: "novel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "novel_chapter_novel_content_id_fk",
                table: "novel_chapter",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "novel_chapter_novel_id_fk",
                table: "novel_chapter",
                column: "novel_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "config");

            migrationBuilder.DropTable(
                name: "config_type");

            migrationBuilder.DropTable(
                name: "novel_chapter");

            migrationBuilder.DropTable(
                name: "novel_content");

            migrationBuilder.DropTable(
                name: "novel");
        }
    }
}
