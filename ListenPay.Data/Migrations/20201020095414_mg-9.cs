using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DurationInSeconds",
                table: "YouTubeVideo",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "DurationYTformat",
                table: "YouTubeVideo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserVideoWatchActivity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    UserInformationId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    YouTubeVideoId = table.Column<int>(nullable: false),
                    WatchTimeInSeconds = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideoWatchActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVideoWatchActivity_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserVideoWatchActivity_UserInformation_UserInformationId",
                        column: x => x.UserInformationId,
                        principalTable: "UserInformation",
                        principalColumn: "UserInformationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserVideoWatchActivity_YouTubeVideo_YouTubeVideoId",
                        column: x => x.YouTubeVideoId,
                        principalTable: "YouTubeVideo",
                        principalColumn: "YouTubeVideoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoWatchActivity_CompanyId",
                table: "UserVideoWatchActivity",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoWatchActivity_UserInformationId",
                table: "UserVideoWatchActivity",
                column: "UserInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoWatchActivity_YouTubeVideoId",
                table: "UserVideoWatchActivity",
                column: "YouTubeVideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVideoWatchActivity");

            migrationBuilder.DropColumn(
                name: "DurationInSeconds",
                table: "YouTubeVideo");

            migrationBuilder.DropColumn(
                name: "DurationYTformat",
                table: "YouTubeVideo");
        }
    }
}
