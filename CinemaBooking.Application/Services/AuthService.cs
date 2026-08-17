using CinemaBooking.Application.Common;
using CinemaBooking.Application.DTOs.Auth;
using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;

namespace CinemaBooking.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        ValidateCredentials(dto.FullName, dto.Email, dto.Password);

        if (await _userRepository.ExistsByEmailAsync(dto.Email))
        {
            throw new BusinessRuleException("An account with this email already exists.");
        }

        var user = new User
        {
            FullName = dto.FullName.Trim(),
            Email = dto.Email.Trim().ToLowerInvariant(),
            PasswordHash = _passwordService.Hash(dto.Password),
            Role = UserRole.Customer,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return CreateResponse(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new BusinessRuleException("Email and password are required.");
        }

        var user = await _userRepository.GetByEmailAsync(dto.Email.Trim().ToLowerInvariant());
        if (user is null || !_passwordService.Verify(user.PasswordHash, dto.Password))
        {
            throw new BusinessRuleException("Invalid email or password.");
        }

        return CreateResponse(user);
    }

    private AuthResponseDto CreateResponse(User user)
    {
        var token = _jwtTokenGenerator.Generate(user);

        return new AuthResponseDto
        {
            Token = token.Token,
            ExpiresAtUtc = token.ExpiresAtUtc,
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

    private static void ValidateCredentials(string fullName, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new BusinessRuleException("Full name is required.");
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new BusinessRuleException("A valid email is required.");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            throw new BusinessRuleException("Password must be at least 8 characters.");
        }
    }
}
