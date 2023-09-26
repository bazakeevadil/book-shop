using Application.Shared;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(
                configuration.GetConnectionString("Default")));

        services.AddIdentityCore<IdentityUser>(opts =>
        {
            opts.User.RequireUniqueEmail = true;
            opts.SignIn.RequireConfirmedEmail = false;
            opts.SignIn.RequireConfirmedAccount = false;
            opts.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
