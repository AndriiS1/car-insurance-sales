using CarInsuranceSales.UseCases.Services.TelegramBot;
using CarInsuranceSales.UseCases.Services.TelegramBot.Options;
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
    }
    
    private static void AddMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatrMaker>());
    }

    private static void AddTelegramBot(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var optionsSection = configuration.GetSection(TelegramBotOptions.Section);
        var telegramBotOptions = optionsSection.Get<TelegramBotOptions>();

        if (telegramBotOptions == null)
        {
            throw new ArgumentNullException(nameof(telegramBotOptions));
        }
        
        builder.Services.AddSingleton<ITelegramBotClient>(_ =>
            new TelegramBotClient(telegramBotOptions.ApiToken));
        builder.Services.AddHostedService<TelegramBotService>();
    }
}
