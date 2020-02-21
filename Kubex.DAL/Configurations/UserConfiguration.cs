using Kubex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubex.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne<License>(u => u.License)
                .WithOne(l => l.User)
                .HasForeignKey<License>(l => l.UserId);
        }
    }
}