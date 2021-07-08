using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    PaymentMethodTypeTitle = table.Column<string>(nullable: true),
                    UserInformationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentMethod");
        }
    }
}
