using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ActivityLog",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_CompanyId",
                table: "ActivityLog",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLog_Company_CompanyId",
                table: "ActivityLog",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLog_Company_CompanyId",
                table: "ActivityLog");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLog_CompanyId",
                table: "ActivityLog");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ActivityLog");
        }
    }
}
