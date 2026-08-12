using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBooking.Infrastructure.Data.Configurations;

public class BookingConfiguration
    : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
     //   builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingReference)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(b => b.BookingReference)
            .IsUnique();

        builder.Property(b => b.CustomerName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(b => b.CustomerEmail)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(b => b.BookingDate)
            .IsRequired();

        builder.Property(b => b.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired();

        builder.HasOne(b => b.Screening)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.ScreeningId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}