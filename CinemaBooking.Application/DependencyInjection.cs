using CinemaBooking.Application.Interfaces;
using CinemaBooking.Application.Mappings;
using CinemaBooking.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MovieProfile).Assembly));

        services.AddScoped<ICinemaService, CinemaService>();
        services.AddScoped<IScreenService, ScreenService>();
        services.AddScoped<ISeatService, SeatService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IScreeningService, ScreeningService>();
        services.AddScoped<IBookingService, BookingService>();

        return services;
    }
}
