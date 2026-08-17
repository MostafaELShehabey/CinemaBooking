using System;
using System.Collections.Generic;
using System.Text;
namespace CinemaBooking.Application.Interfaces.Identity;

public interface IIdentityService
{
    Task<IdentityUserResult?> CreateUserAsync(
        string fullName,
        string email,
        string password,
        string role);

    Task<IdentityUserResult?> AuthenticateAsync(
        string email,
        string password);

    Task<bool> UserExistsAsync(string email);
}

public record IdentityUserResult(
    string Id,
    string FullName,
    string Email,
    string Role);