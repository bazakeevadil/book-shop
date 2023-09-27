using Application.Shared;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
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
                configuration.GetConnectionString("SqlConnection")));

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IBookRepository, BookRepository>();

        return services;
    }
}
