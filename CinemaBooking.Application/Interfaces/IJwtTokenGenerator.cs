using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Interfaces;

public record JwtTokenResult(string Token, DateTime ExpiresAtUtc);

public interface IJwtTokenGenerator
{
    JwtTokenResult Generate(User user);
}
