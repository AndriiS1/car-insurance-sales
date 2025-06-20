using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.User;
using CarInsuranceSales.Infrastructure.Database;
using CarInsuranceSales.Infrastructure.Database.Repositories;
using CarInsuranceSales.Infrastructure.Options;
using CarInsuranceSales.Infrastructure.Services.FileService;
using CarInsuranceSales.Infrastructure.Services.PassportService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mindee.Extensions.DependencyInjection;
namespace CarInsuranceSales.Infrastructure;

public static class HostBuilderExtensions
{
    public static void ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        builder.ConfigureDatabase();
        builder.ConfigureRepositories();
        builder.AddServices();
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
        builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
        builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
    }

    private static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IFileService, FileService>();
        builder.AddOcrService();
    }
    
    private static void AddOcrService(this WebApplicationBuilder builder)
    {
        builder.Services.AddMindeeClient();
        builder.Services.AddScoped<IOcrService, OcrService>();
    }
}
