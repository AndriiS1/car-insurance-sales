using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Rules;
using CarInsuranceSales.UseCases.Services.FileService;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc;

public class UploadVehicleDocCommandHandler(ITelegramBotClient botClient, IFileService fileService,
    IDocumentRepository documentRepository, IConversationRepository conversationRepository) : IRequestHandler<UploadVehicleDocCommand>
{
    public async Task Handle(UploadVehicleDocCommand request, CancellationToken cancellationToken)
    {
        if(request.Message.Document == null)
        {
            await botClient.SendMessage(request.Message.Chat.Id, "❌ Please upload your upload vehicle information as a document.", cancellationToken: cancellationToken);
            return;
        }

        var currentConversation = await conversationRepository.GetLastByUserId(request.User.Id);
        var fileId = request.Message.Document.FileId;
        
        var file = await botClient.GetFile(fileId, cancellationToken);
        
        var extension = Path.GetExtension(file.FilePath) ?? ".jpg";
        
        var document = new Document
        {
            Id = Guid.NewGuid(),
            Type = DocumentType.VehicleDocument,
            ConversationId = currentConversation!.Id,
            ExternalFileId = fileId,
            Extension = extension,
            FilePath =  VehicleDocRule.GetVehicleDocPath(request.User.Id,  currentConversation.Id, extension),
            ExternalFilePath = file.FilePath!
        };

        var keyboard = new InlineKeyboardMarkup([
            [
                InlineKeyboardButton.WithCallbackData("✅ Continue", "vehicle_doc:continue"),
                InlineKeyboardButton.WithCallbackData("❌ Reupload", "vehicle_doc:reupload"),
            ]
        ]);
        
        await documentRepository.Upsert(document);
        await documentRepository.SaveChangesAsync();
        
        await fileService.SaveFile(document, request.User, cancellationToken);

        await botClient.SendMessage(request.Message.Chat.Id, "✅ Vehicle doc received.", replyMarkup:keyboard, cancellationToken: cancellationToken);
    }
}
