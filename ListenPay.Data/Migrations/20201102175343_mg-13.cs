using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRelatedVideoData",
                columns: table => new
                {
                    UserRelatedDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    UserInformationId = table.Column<int>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    YouTubeVideoId = table.Column<int>(nullable: false),
                    YouTubeVideoCategoryId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelatedVideoData", x => x.UserRelatedDataId);
                    table.ForeignKey(
                        name: "FK_UserRelatedVideoData_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRelatedVideoData_UserInformation_UserInformationId",
                        column: x => x.UserInformationId,
                        principalTable: "UserInformation",
                        principalColumn: "UserInformationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRelatedVideoData_YouTubeVideoCategory_YouTubeVideoCategoryId",
                        column: x => x.YouTubeVideoCategoryId,
                        principalTable: "YouTubeVideoCategory",
                        principalColumn: "YouTubeVideoCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRelatedVideoData_YouTubeVideo_YouTubeVideoId",
                        column: x => x.YouTubeVideoId,
                        principalTable: "YouTubeVideo",
                        principalColumn: "YouTubeVideoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedVideoData_CompanyId",
                table: "UserRelatedVideoData",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedVideoData_UserInformationId",
                table: "UserRelatedVideoData",
                column: "UserInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedVideoData_YouTubeVideoCategoryId",
                table: "UserRelatedVideoData",
                column: "YouTubeVideoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedVideoData_YouTubeVideoId",
                table: "UserRelatedVideoData",
                column: "YouTubeVideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRelatedVideoData");
        }
    }
}
