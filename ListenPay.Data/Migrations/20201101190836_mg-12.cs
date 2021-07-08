using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryMatrics",
                columns: table => new
                {
                    CategoryMatricsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    YouTubeVideoCategoryId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    TargetMean = table.Column<double>(nullable: false),
                    ReportedAccident = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMatrics", x => x.CategoryMatricsId);
                    table.ForeignKey(
                        name: "FK_CategoryMatrics_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryMatrics_YouTubeVideoCategory_YouTubeVideoCategoryId",
                        column: x => x.YouTubeVideoCategoryId,
                        principalTable: "YouTubeVideoCategory",
                        principalColumn: "YouTubeVideoCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMatrics_CompanyId",
                table: "CategoryMatrics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMatrics_YouTubeVideoCategoryId",
                table: "CategoryMatrics",
                column: "YouTubeVideoCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMatrics");
        }
    }
}
