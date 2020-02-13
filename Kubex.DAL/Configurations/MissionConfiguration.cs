using Kubex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubex.DAL.Configurations
{
    public class MissionConfiguration : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
            builder.HasKey(key => new { key.CheckpointId, key.RoundId });

            builder.HasOne(m => m.Checkpoint)
                .WithMany(c => c.Missions)
                .HasForeignKey(m => m.CheckpointId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Round)
                .WithMany(r => r.Missions)
                .HasForeignKey(m => m.RoundId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}