using Telegram.Bot.Types;
namespace CarInsuranceSales.UseCases.Services.CommandProcessor;

public interface ICommandProcessor
{
    Task Process(Update update);
}
