using Microsoft.AspNetCore.Authorization;

namespace CinemaBooking.API.Extensions;

public static class AuthorizationExtensions
{
    public const string AdminPolicy = "AdminOnly";
    public const string CustomerPolicy = "CustomerOnly";

    public static IServiceCollection AddApplicationAuthorization(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                AdminPolicy,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                });

            options.AddPolicy(
                CustomerPolicy,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Customer");
                });
        });

        return services;
    }
}