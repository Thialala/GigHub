using GigHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Data.Configuration
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(a => new { a.GigId, a.AttendeeId });
            builder.Property(a => a.AttendeeId).HasMaxLength(450);
            builder.HasOne(a => a.Gig)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}