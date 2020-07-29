using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class AddDARColumnToCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "DailyActivityReports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyActivityReports_CompanyId",
                table: "DailyActivityReports",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyActivityReports_Companies_CompanyId",
                table: "DailyActivityReports",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyActivityReports_Companies_CompanyId",
                table: "DailyActivityReports");

            migrationBuilder.DropIndex(
                name: "IX_DailyActivityReports_CompanyId",
                table: "DailyActivityReports");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DailyActivityReports");
        }
    }
}
