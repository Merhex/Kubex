using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class AddCustomerNumberColumnToCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerNumber",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerNumber",
                table: "Companies");
        }
    }
}
