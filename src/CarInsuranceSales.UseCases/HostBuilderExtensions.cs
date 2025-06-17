using CarInsuranceSales.UseCases.Options;
using CarInsuranceSales.UseCases.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases;

public static class HostBuilderExtensions
{
    public static void ConfigureUseCases(this WebApplicationBuilder builder)
    {
        builder.AddMediatr();
        builder.AddTelegramBot();
        builder.AddServices();
    }
    
    private static void AddMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatrMaker>());
    }

    private static void AddTelegramBot(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ITelegramBotClient>(_ =>
            new TelegramBotClient(GetOptions(builder.Configuration).ApiToken));
    }

    private static TelegramBotOptions GetOptions(IConfiguration configuration)
    {
        var optionsSection = configuration.GetSection(TelegramBotOptions.Section);
        var telegramBotOptions = optionsSection.Get<TelegramBotOptions>();

        if (telegramBotOptions == null)
        {
            throw new ArgumentNullException(nameof(telegramBotOptions));
        }

        return telegramBotOptions;
    }

    private static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICommandProcessor, CommandProcessor>();
    }
    
    public static async Task SetWebhookUrl(this WebApplication webApplication)
    {
        var botClient = webApplication.Services.GetRequiredService<ITelegramBotClient>();
        
        await botClient.SetWebhook(GetOptions(webApplication.Configuration).WebhookUrl);
    }
}
