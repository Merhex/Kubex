using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class AddCompanyToDARTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyActivityReports_Companies_CompanyId",
                table: "DailyActivityReports");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "DailyActivityReports",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyActivityReports_Companies_CompanyId",
                table: "DailyActivityReports",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyActivityReports_Companies_CompanyId",
                table: "DailyActivityReports");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "DailyActivityReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DailyActivityReports_Companies_CompanyId",
                table: "DailyActivityReports",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
