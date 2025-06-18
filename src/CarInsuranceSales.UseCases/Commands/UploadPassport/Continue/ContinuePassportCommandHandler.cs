using CarInsuranceSales.Domain.Models.User;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport.Continue;

public class ContinuePassportCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository) : IRequestHandler<ContinuePassportCommand>
{
    public async Task Handle(ContinuePassportCommand request, CancellationToken cancellationToken)
    {
        request.User.CurrentState = UserState.WaitingForVehicleDoc;
        
        await userRepository.SaveChangesAsync();
        await botClient.SendMessage(request.User.ExternalUserId, "Now, please upload your vehicle registration certificate as document.", cancellationToken: cancellationToken);
        await botClient.EditMessageReplyMarkup(
            chatId: request.User.ExternalUserId,
            messageId: request.Callback.Message!.MessageId,
            replyMarkup: null,
            cancellationToken: cancellationToken);
        await botClient.AnswerCallbackQuery(request.Callback.Id, cancellationToken: cancellationToken);
    }
}
