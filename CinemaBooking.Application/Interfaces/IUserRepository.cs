using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);

    Task<User?> GetByEmailAsync(string email);

    Task<bool> ExistsByEmailAsync(string email);

    Task AddAsync(User user);

    Task SaveChangesAsync();
}
