using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBooking.Infrastructure.Data.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
     //   builder.ToTable("Seats");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.RowNumber)
            .IsRequired();

        builder.Property(s => s.SeatNumber)
            .IsRequired();

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.HasOne(s => s.Screen)
            .WithMany(s => s.Seats)
            .HasForeignKey(s => s.ScreenId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => new
        {
            s.ScreenId,
            s.RowNumber,
            s.SeatNumber
        })
        .IsUnique();
    }
}