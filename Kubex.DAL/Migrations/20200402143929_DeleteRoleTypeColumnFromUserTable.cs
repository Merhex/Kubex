using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class DeleteRoleTypeColumnFromUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RoleTypes_RoletypeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoletypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoletypeId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoletypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoletypeId",
                table: "AspNetUsers",
                column: "RoletypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RoleTypes_RoletypeId",
                table: "AspNetUsers",
                column: "RoletypeId",
                principalTable: "RoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
