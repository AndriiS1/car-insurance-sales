using CarInsuranceSales.UseCases.Commands.Start;
using MediatR;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Services.TelegramBot;

public class TelegramBotService(ITelegramBotClient botClient, IMediator mediator) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        botClient.StartReceiving(
            async (_, update, ct) =>
            {
                if (update.Message?.Text == "/start")
                {
                    await mediator.Send(new StartCommand(update), ct);
                }
            },
            (_, exception, _) =>
            {
                Console.WriteLine($"Bot error: {exception.Message}");
                return Task.CompletedTask;
            },
            cancellationToken: cancellationToken
        );

        Console.WriteLine("Bot started...");
    }
}
