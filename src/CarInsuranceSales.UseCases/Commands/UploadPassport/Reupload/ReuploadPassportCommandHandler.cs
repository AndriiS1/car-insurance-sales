using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport.Reupload;

public class ReuploadPassportCommandHandler(ITelegramBotClient botClient) : IRequestHandler<ReuploadPassportCommand>
{
    public async Task Handle(ReuploadPassportCommand request, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(request.User.ExternalUserId, "Please reupload you passport as an image again", cancellationToken: cancellationToken);
        await botClient.EditMessageReplyMarkup(
            chatId: request.User.ExternalUserId,
            messageId: request.Callback.Message!.MessageId,
            replyMarkup: null,
            cancellationToken: cancellationToken);
        await botClient.AnswerCallbackQuery(request.Callback.Id, cancellationToken: cancellationToken);
    }
}
