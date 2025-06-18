using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.User;
using CarInsuranceSales.Domain.Rules;
using CarInsuranceSales.UseCases.Services.FileService;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc;

public class UploadVehicleDocCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository, IFileService fileService,
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
                InlineKeyboardButton.WithCallbackData("✅ Continue", "doc:confirm"),
            ]
        ]);
        
        await Task.WhenAll(CreateDocumentAsync(document, request.User, cancellationToken), UpdateUser(request.User));

        await botClient.SendMessage(request.Message.Chat.Id, "✅ Vehicle doc received. Now, please upload your vehicle registration certificate.", cancellationToken: cancellationToken);
    }
    
    private async Task CreateDocumentAsync(Document document, User user, CancellationToken cancellationToken)
    {
        await documentRepository.Upsert(document);
        await documentRepository.SaveChangesAsync();
        
        await fileService.SaveFile(document, user, cancellationToken);
    }

    private async Task UpdateUser(User user)
    {
        user.CurrentState = UserState.WaitingForVehicleDoc;
        
        await userRepository.SaveChangesAsync();
    }
}
