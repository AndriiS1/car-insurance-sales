using CarInsuranceSales.Domain.Models.User;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.Cancel;

public class CancelCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository) : IRequestHandler<CancelCommand>
{
    public async Task Handle(CancelCommand request, CancellationToken cancellationToken)
    {
        const string canceledText = "The flow is canceled. To start over input /start command.";

        request.User.CurrentState = UserState.Idle;

        await userRepository.SaveChangesAsync();
        
        await botClient.SendMessage(
            chatId: request.Message.Chat.Id,
            text: canceledText,
            cancellationToken: cancellationToken
        );
    }
}
