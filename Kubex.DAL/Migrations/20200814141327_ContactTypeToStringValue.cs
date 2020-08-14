using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class ContactTypeToStringValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactTypes_ContactTypeId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactTypeId",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Contacts");

            migrationBuilder.AddColumn<byte>(
                name: "ContactTypeId",
                table: "Contacts",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactTypes_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
