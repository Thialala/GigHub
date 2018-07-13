using GigHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Data.Configuration
{
    public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure(EntityTypeBuilder<Following> builder)
        {
            builder.HasKey(f => new { f.FolloweeId, f.FollowerId });

            builder.HasOne(f => f.Followee)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Follower)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}