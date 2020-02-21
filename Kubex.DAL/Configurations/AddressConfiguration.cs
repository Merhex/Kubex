using Kubex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubex.DAL.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .HasIndex(a => new 
                { 
                    a.AppartementBus,
                    a.CountryId,
                    a.HouseNumber,
                    a.StreetId,
                    a.ZIPId
                })
                .IsUnique();
        }
    }
}