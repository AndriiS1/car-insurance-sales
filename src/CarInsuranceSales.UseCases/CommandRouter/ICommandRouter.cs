using Telegram.Bot.Types;
namespace CarInsuranceSales.UseCases.CommandRouter;

public interface ICommandRouter
{
    Task Process(Update update);
}
