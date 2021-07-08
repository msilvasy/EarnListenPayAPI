using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YouTubeVideo",
                columns: table => new
                {
                    YouTubeVideoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    VideoId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ThumbnailURL = table.Column<string>(nullable: true),
                    VideoURL = table.Column<string>(nullable: false),
                    UserInformationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeVideo", x => x.YouTubeVideoId);
                    table.ForeignKey(
                        name: "FK_YouTubeVideo_UserInformation_UserInformationId",
                        column: x => x.UserInformationId,
                        principalTable: "UserInformation",
                        principalColumn: "UserInformationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YouTubeVideoCategory",
                columns: table => new
                {
                    YouTubeVideoCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeVideoCategory", x => x.YouTubeVideoCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "UserRelatedYouTubeCategories",
                columns: table => new
                {
                    UserRelatedYouTubeCategoriesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    YouTubeCategoryId = table.Column<int>(nullable: false),
                    UserInformationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelatedYouTubeCategories", x => x.UserRelatedYouTubeCategoriesId);
                    table.ForeignKey(
                        name: "FK_UserRelatedYouTubeCategories_UserInformation_UserInformationId",
                        column: x => x.UserInformationId,
                        principalTable: "UserInformation",
                        principalColumn: "UserInformationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRelatedYouTubeCategories_YouTubeVideoCategory_YouTubeCategoryId",
                        column: x => x.YouTubeCategoryId,
                        principalTable: "YouTubeVideoCategory",
                        principalColumn: "YouTubeVideoCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YouTubeVideoRelatedCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    YouTubeVideoCategoryId = table.Column<int>(nullable: false),
                    YouTubeVideoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeVideoRelatedCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YouTubeVideoRelatedCategories_YouTubeVideoCategory_YouTubeVideoCategoryId",
                        column: x => x.YouTubeVideoCategoryId,
                        principalTable: "YouTubeVideoCategory",
                        principalColumn: "YouTubeVideoCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YouTubeVideoRelatedCategories_YouTubeVideo_YouTubeVideoId",
                        column: x => x.YouTubeVideoId,
                        principalTable: "YouTubeVideo",
                        principalColumn: "YouTubeVideoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedYouTubeCategories_UserInformationId",
                table: "UserRelatedYouTubeCategories",
                column: "UserInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedYouTubeCategories_YouTubeCategoryId",
                table: "UserRelatedYouTubeCategories",
                column: "YouTubeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_YouTubeVideo_UserInformationId",
                table: "YouTubeVideo",
                column: "UserInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_YouTubeVideoRelatedCategories_YouTubeVideoCategoryId",
                table: "YouTubeVideoRelatedCategories",
                column: "YouTubeVideoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_YouTubeVideoRelatedCategories_YouTubeVideoId",
                table: "YouTubeVideoRelatedCategories",
                column: "YouTubeVideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRelatedYouTubeCategories");

            migrationBuilder.DropTable(
                name: "YouTubeVideoRelatedCategories");

            migrationBuilder.DropTable(
                name: "YouTubeVideoCategory");

            migrationBuilder.DropTable(
                name: "YouTubeVideo");
        }
    }
}
