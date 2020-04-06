using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class AddUniqueConstraintToZipStreetCountryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_RoleTypes_RoleTypeId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleTypes",
                table: "RoleTypes");

            migrationBuilder.RenameTable(
                name: "RoleTypes",
                newName: "RoleType");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ZIPCodes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Streets",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleType",
                table: "RoleType",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ZIPCodes_Code",
                table: "ZIPCodes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_Name",
                table: "Streets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_RoleType_RoleTypeId",
                table: "Teams",
                column: "RoleTypeId",
                principalTable: "RoleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_RoleType_RoleTypeId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_ZIPCodes_Code",
                table: "ZIPCodes");

            migrationBuilder.DropIndex(
                name: "IX_Streets_Name",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Name",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleType",
                table: "RoleType");

            migrationBuilder.RenameTable(
                name: "RoleType",
                newName: "RoleTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ZIPCodes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Streets",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleTypes",
                table: "RoleTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_RoleTypes_RoleTypeId",
                table: "Teams",
                column: "RoleTypeId",
                principalTable: "RoleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
