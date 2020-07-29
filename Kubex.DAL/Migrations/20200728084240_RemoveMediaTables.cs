using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class RemoveMediaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_LicenseTypes_LicenseTypeId",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_AspNetUsers_UserId",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_MediaTypes_MediaTypeId",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaTypes",
                table: "MediaTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LicenseTypes",
                table: "LicenseTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Licenses",
                table: "Licenses");

            migrationBuilder.RenameTable(
                name: "MediaTypes",
                newName: "MediaType");

            migrationBuilder.RenameTable(
                name: "LicenseTypes",
                newName: "LicenseType");

            migrationBuilder.RenameTable(
                name: "Licenses",
                newName: "License");

            migrationBuilder.RenameIndex(
                name: "IX_Licenses_UserId",
                table: "License",
                newName: "IX_License_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Licenses_LicenseTypeId",
                table: "License",
                newName: "IX_License_LicenseTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaType",
                table: "MediaType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LicenseType",
                table: "LicenseType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_License",
                table: "License",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_License_LicenseType_LicenseTypeId",
                table: "License",
                column: "LicenseTypeId",
                principalTable: "LicenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_License_AspNetUsers_UserId",
                table: "License",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_MediaType_MediaTypeId",
                table: "Media",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_License_LicenseType_LicenseTypeId",
                table: "License");

            migrationBuilder.DropForeignKey(
                name: "FK_License_AspNetUsers_UserId",
                table: "License");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_MediaType_MediaTypeId",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaType",
                table: "MediaType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LicenseType",
                table: "LicenseType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_License",
                table: "License");

            migrationBuilder.RenameTable(
                name: "MediaType",
                newName: "MediaTypes");

            migrationBuilder.RenameTable(
                name: "LicenseType",
                newName: "LicenseTypes");

            migrationBuilder.RenameTable(
                name: "License",
                newName: "Licenses");

            migrationBuilder.RenameIndex(
                name: "IX_License_UserId",
                table: "Licenses",
                newName: "IX_Licenses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_License_LicenseTypeId",
                table: "Licenses",
                newName: "IX_Licenses_LicenseTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaTypes",
                table: "MediaTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LicenseTypes",
                table: "LicenseTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Licenses",
                table: "Licenses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_LicenseTypes_LicenseTypeId",
                table: "Licenses",
                column: "LicenseTypeId",
                principalTable: "LicenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_AspNetUsers_UserId",
                table: "Licenses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_MediaTypes_MediaTypeId",
                table: "Media",
                column: "MediaTypeId",
                principalTable: "MediaTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
