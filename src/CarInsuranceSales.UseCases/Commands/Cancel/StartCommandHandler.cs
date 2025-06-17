using CarInsuranceSales.Domain.Models.User;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.Cancel;

public class StartCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository) : IRequestHandler<Cancel.CancelCommand>
{
    public async Task Handle(Cancel.CancelCommand request, CancellationToken cancellationToken)
    {
        const string welcomeText = "The flow is canceled. To start over input /start command.";

        request.User.CurrentState = UserState.Idle;

        await userRepository.SaveChangesAsync();
        
        await botClient.SendMessage(
            chatId: request.Message.Chat.Id,
            text: welcomeText,
            cancellationToken: cancellationToken
        );
    }
}
