using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class TelePortalAccountKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "TelePortalAccount");

            migrationBuilder.AddColumn<int>(
                name: "UserInformationId",
                table: "TelePortalAccount",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelePortalAccount_UserInformationId",
                table: "TelePortalAccount",
                column: "UserInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TelePortalAccount_UserInformation_UserInformationId",
                table: "TelePortalAccount",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "UserInformationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelePortalAccount_UserInformation_UserInformationId",
                table: "TelePortalAccount");

            migrationBuilder.DropIndex(
                name: "IX_TelePortalAccount_UserInformationId",
                table: "TelePortalAccount");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "TelePortalAccount");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TelePortalAccount",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
