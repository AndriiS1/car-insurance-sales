using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Rules;
using CarInsuranceSales.Infrastructure.Services.FileService;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport;

public class UploadPassportCommandHandler(ITelegramBotClient botClient, IFileService fileService,
    IDocumentRepository documentRepository, IConversationRepository conversationRepository) : IRequestHandler<UploadPassportCommand>
{
    public async Task Handle(UploadPassportCommand request, CancellationToken cancellationToken)
    {
        if(request.Message.Photo == null || request.Message.Photo.Length < 1)
        {
            await botClient.SendMessage(request.Message.Chat.Id, "❌ Please upload your passport as a photo.", cancellationToken: cancellationToken);
            return;
        }

        var currentConversation = await conversationRepository.GetLastByUserId(request.User.Id);
        var fileId = request.Message.Photo.Last().FileId;
        
        var file = await botClient.GetFile(fileId, cancellationToken);
        
        var extension = Path.GetExtension(file.FilePath) ?? ".jpg";
        
        var document = new Document
        {
            Id = Guid.NewGuid(),
            Type = DocumentType.Passport,
            ConversationId = currentConversation!.Id,
            ExternalFileId = fileId,
            Extension = extension,
            FilePath =  PassportRule.GetVehicleDocPath(request.User.Id,  currentConversation.Id, extension),
            ExternalFilePath = file.FilePath!
        };

        await documentRepository.Upsert(document);
        await documentRepository.SaveChangesAsync();
        
        await fileService.SaveFile(document, cancellationToken);
        
        var keyboard = new InlineKeyboardMarkup([
            [
                InlineKeyboardButton.WithCallbackData("✅ Continue", "doc:continue"),
                InlineKeyboardButton.WithCallbackData("❌ Reupload", "doc:reupload"),
            ]
        ]);

        await botClient.SendMessage(request.Message.Chat.Id, "✅ Passport received.", replyMarkup: keyboard, cancellationToken: cancellationToken);
    }
}
