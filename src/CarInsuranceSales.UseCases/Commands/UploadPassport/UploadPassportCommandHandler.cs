using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.User;
using CarInsuranceSales.Domain.Rules;
using CarInsuranceSales.UseCases.Services.FileService;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport;

public class UploadPassportCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository, IFileService fileService,
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

        await Task.WhenAll(CreateDocumentAsync(document, request.User, cancellationToken), UpdateUser(request.User));

        await botClient.SendMessage(request.Message.Chat.Id, "✅ Passport received. Now, please upload your vehicle registration certificate as document.", cancellationToken: cancellationToken);
    }

    private async Task CreateDocumentAsync(Document document, User user, CancellationToken cancellationToken)
    {
        await documentRepository.Create(document);
        await documentRepository.SaveChangesAsync();
        
        await fileService.SaveFile(document, user, cancellationToken);
    }

    private async Task UpdateUser(User user)
    {
        user.CurrentState = UserState.WaitingForVehicleDoc;
        
        await userRepository.SaveChangesAsync();
    }
}
