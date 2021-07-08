using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyRelatedYouTubeCategories",
                table: "CompanyRelatedYouTubeCategories");

            migrationBuilder.DropColumn(
                name: "UserRelatedYouTubeCategoriesId",
                table: "CompanyRelatedYouTubeCategories");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "YouTubeVideo",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "TelePortalAccount",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ComapnyRelatedYouTubeCategoriesId",
                table: "CompanyRelatedYouTubeCategories",
                nullable: false,
                defaultValue: 1)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyRelatedYouTubeCategories",
                table: "CompanyRelatedYouTubeCategories",
                column: "ComapnyRelatedYouTubeCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_YouTubeVideo_CompanyId",
                table: "YouTubeVideo",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TelePortalAccount_CompanyId",
                table: "TelePortalAccount",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TelePortalAccount_Company_CompanyId",
                table: "TelePortalAccount",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YouTubeVideo_Company_CompanyId",
                table: "YouTubeVideo",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelePortalAccount_Company_CompanyId",
                table: "TelePortalAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_YouTubeVideo_Company_CompanyId",
                table: "YouTubeVideo");

            migrationBuilder.DropIndex(
                name: "IX_YouTubeVideo_CompanyId",
                table: "YouTubeVideo");

            migrationBuilder.DropIndex(
                name: "IX_TelePortalAccount_CompanyId",
                table: "TelePortalAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyRelatedYouTubeCategories",
                table: "CompanyRelatedYouTubeCategories");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "YouTubeVideo");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TelePortalAccount");

            migrationBuilder.DropColumn(
                name: "ComapnyRelatedYouTubeCategoriesId",
                table: "CompanyRelatedYouTubeCategories");

            migrationBuilder.AddColumn<int>(
                name: "UserRelatedYouTubeCategoriesId",
                table: "CompanyRelatedYouTubeCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyRelatedYouTubeCategories",
                table: "CompanyRelatedYouTubeCategories",
                column: "UserRelatedYouTubeCategoriesId");
        }
    }
}
