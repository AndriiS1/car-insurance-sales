using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc.Reupload;

public class ReuploadVehicleDocCommandHandler(ITelegramBotClient botClient) : IRequestHandler<ReuploadVehicleDocCommand>
{
    public async Task Handle(ReuploadVehicleDocCommand request, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(request.User.ExternalUserId, "Please reupload you vehicle document as an image again.", cancellationToken: cancellationToken);
        await botClient.EditMessageReplyMarkup(
            chatId: request.User.ExternalUserId,
            messageId: request.Callback.Message!.MessageId,
            replyMarkup: null,
            cancellationToken: cancellationToken);
        await botClient.AnswerCallbackQuery(request.Callback.Id, cancellationToken: cancellationToken);
    }
}
