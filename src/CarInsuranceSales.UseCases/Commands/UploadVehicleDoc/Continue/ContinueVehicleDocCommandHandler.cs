using System.Text.Json;
using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.User;
using CarInsuranceSales.Infrastructure.Services.PassportService;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc.Continue;

public class ContinueVehicleDocCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository, IOcrService ocrService,
    IConversationRepository conversationRepository) : IRequestHandler<ContinueVehicleDocCommand>
{
    public async Task Handle(ContinueVehicleDocCommand request, CancellationToken cancellationToken)
    {
        await botClient.AnswerCallbackQuery(request.Callback.Id, cancellationToken: cancellationToken);
        await botClient.EditMessageReplyMarkup(
            chatId: request.User.ExternalUserId,
            messageId: request.Callback.Message!.MessageId,
            replyMarkup: null,
            cancellationToken: cancellationToken);
        request.User.CurrentState = UserState.ConfirmingOCR;
        
        await userRepository.SaveChangesAsync();
        await botClient.SendMessage(request.User.ExternalUserId, "Now, confirm the OCR.", cancellationToken: cancellationToken);
        
        var currentConversation = await conversationRepository.GetLastByUserId(request.User.Id);

        var conversationPassport = currentConversation!.Documents.First(c => c.Type == DocumentType.Passport);
            
        var passportResponse = await ocrService.GetDocumentData(conversationPassport);
        
        await botClient.SendMessage(request.User.ExternalUserId, $"Now, confirm the OCR. {JsonSerializer.Serialize(passportResponse)}", cancellationToken: cancellationToken);
    }
}
