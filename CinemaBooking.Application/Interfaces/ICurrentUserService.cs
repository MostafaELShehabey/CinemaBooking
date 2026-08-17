namespace CinemaBooking.Application.Interfaces;

public interface ICurrentUserService
{
    bool IsAuthenticated { get; }

    int UserId { get; }

    string Email { get; }

    string FullName { get; }

    bool IsAdmin { get; }
}
