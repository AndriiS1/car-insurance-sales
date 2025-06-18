using CarInsuranceSales.Domain.Models.User;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc.Continue;

public class ContinueVehicleDocCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository) : IRequestHandler<ContinueVehicleDocCommand>
{
    public async Task Handle(ContinueVehicleDocCommand request, CancellationToken cancellationToken)
    {
        request.User.CurrentState = UserState.ConfirmingOCR;
        
        await userRepository.SaveChangesAsync();
        await botClient.SendMessage(request.User.ExternalUserId, "Now, confirm the OCR.", cancellationToken: cancellationToken);
        await botClient.EditMessageReplyMarkup(
            chatId: request.User.ExternalUserId,
            messageId: request.Callback.Message!.MessageId,
            replyMarkup: null,
            cancellationToken: cancellationToken);
        await botClient.AnswerCallbackQuery(request.Callback.Id, cancellationToken: cancellationToken);
    }
}
