using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class MoveDARColumnFromCompanyToPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "DailyActivityReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DailyActivityReports_PostId",
                table: "DailyActivityReports",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyActivityReports_Posts_PostId",
                table: "DailyActivityReports",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyActivityReports_Posts_PostId",
                table: "DailyActivityReports");

            migrationBuilder.DropIndex(
                name: "IX_DailyActivityReports_PostId",
                table: "DailyActivityReports");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "DailyActivityReports");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "DailyActivityReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
