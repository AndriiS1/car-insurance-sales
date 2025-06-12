using CarInsuranceSales.Domain.Models.User;
using CarInsuranceSales.Infrastructure.Database;
using CarInsuranceSales.Infrastructure.Database.Repositories;
using CarInsuranceSales.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CarInsuranceSales.Infrastructure;

public static class HostBuilderExtensions
{
    public static void ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        builder.ConfigureDatabase();
        builder.ConfigureRepositories();
    }

    private static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var optionsSection = configuration.GetSection(DataBaseOptions.Section);
        var databaseOptions = optionsSection.Get<DataBaseOptions>();

        if (databaseOptions == null)
        {
            throw new ArgumentNullException(nameof(databaseOptions));
        }
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(databaseOptions.ConnectionString));
    }

    private static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}
