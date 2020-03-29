using Kubex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubex.DAL.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(key => new { key.PostId, key.UserId });

            builder.HasOne(up => up.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(up => up.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(up => up.Post)
                .WithMany(p => p.Teams)
                .HasForeignKey(up => up.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}