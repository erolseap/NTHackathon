using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTHackathon.Application.Repositories;
using NTHackathon.Domain.Repositories;
using NTHackathon.Infrastructure.Data;
using NTHackathon.Infrastructure.Entities;
using NTHackathon.Infrastructure.Repositories;

namespace NTHackathon.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts => opts
            .UseNpgsql(configuration.GetConnectionString("default")!, builder => builder.MigrationsAssembly(Assembly.GetExecutingAssembly()))
            .UseSnakeCaseNamingConvention()
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IReadOnlyRepositoryAsync<>), typeof(ReadOnlyRepositoryAsync<>));
        services.AddScoped(typeof(IWriteRepositoryAsync<>), typeof(WriteRepositoryAsync<>));
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
