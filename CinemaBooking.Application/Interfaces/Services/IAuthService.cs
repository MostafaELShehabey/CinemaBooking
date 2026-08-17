using CinemaBooking.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
