using System.Reflection;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTHackathon.Application.Repositories;
using NTHackathon.Domain.Repositories;
using NTHackathon.Domain.Services;
using NTHackathon.Infrastructure.Data;
using NTHackathon.Infrastructure.Entities;
using NTHackathon.Infrastructure.İnterfaces;
using NTHackathon.Infrastructure.Repositories;
using NTHackathon.Infrastructure.Services;

namespace NTHackathon.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts => opts
            .UseNpgsql(configuration.GetConnectionString("default")!, builder => builder.MigrationsAssembly(Assembly.GetExecutingAssembly()))
            .UseSnakeCaseNamingConvention()
        );
        
        services.AddHttpClient();

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IReadOnlyRepositoryAsync<>), typeof(ReadOnlyRepositoryAsync<>));
        services.AddScoped(typeof(IWriteRepositoryAsync<>), typeof(WriteRepositoryAsync<>));
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>();

        using var dbContext = new AppDbContext(dbContextOptions);
        dbContext.Database.Migrate();
    }

    public static async Task CreateRoles(this IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppUserRole>>();
        var createRoleIfNotExists = async (string roleName) =>
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new AppUserRole(roleName));
            }
        };
        await createRoleIfNotExists("Admin");
    }
}
