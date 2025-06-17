using Telegram.Bot.Types;
namespace CarInsuranceSales.UseCases.Services;

public interface ICommandProcessor
{
    Task Process(Update update);
}
