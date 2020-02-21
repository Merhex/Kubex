using Microsoft.EntityFrameworkCore.Migrations;

namespace Kubex.DAL.Migrations
{
    public partial class AddUniqueContraintToAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppartementBus_CountryId_HouseNumber_StreetId_ZIPId",
                table: "Addresses",
                columns: new[] { "AppartementBus", "CountryId", "HouseNumber", "StreetId", "ZIPId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_AppartementBus_CountryId_HouseNumber_StreetId_ZIPId",
                table: "Addresses");
        }
    }
}
