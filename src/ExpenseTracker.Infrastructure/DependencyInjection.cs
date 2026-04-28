using ExpenseTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure;

public class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExpenseTrackerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}