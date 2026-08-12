using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBooking.Infrastructure.Data.Configurations;

public class BookingSeatConfiguration
    : IEntityTypeConfiguration<BookingSeat>
{
    public void Configure(EntityTypeBuilder<BookingSeat> builder)
    {
      //      builder.ToTable("BookingSeats");

        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(bs => bs.Booking)
            .WithMany(b => b.BookingSeats)
            .HasForeignKey(bs => bs.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bs => bs.Seat)
            .WithMany(s => s.BookingSeats)
            .HasForeignKey(bs => bs.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(bs => new
        {
            bs.BookingId,
            bs.SeatId
        })
        .IsUnique();
    }
}