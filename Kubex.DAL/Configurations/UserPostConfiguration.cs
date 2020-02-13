using Kubex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kubex.DAL.Configurations
{
    public class UserPostConfiguration : IEntityTypeConfiguration<UserPost>
    {
        public void Configure(EntityTypeBuilder<UserPost> builder)
        {
            builder.HasKey(key => new { key.PostId, key.UserId });

            builder.HasOne(up => up.User)
                .WithMany(u => u.UserPosts)
                .HasForeignKey(up => up.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(up => up.Post)
                .WithMany(p => p.UserPosts)
                .HasForeignKey(up => up.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}