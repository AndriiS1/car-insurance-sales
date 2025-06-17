using CarInsuranceSales.Domain.Models.User;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport;

public class UploadPassportCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository) : IRequestHandler<UploadPassportCommand>
{
    public async Task Handle(UploadPassportCommand request, CancellationToken cancellationToken)
    {
        if(request.Message.Document == null)
        {
            await botClient.SendMessage(request.Message.Chat.Id, "❌ Please upload your passport as a document.", cancellationToken: cancellationToken);
            return;
        }

        // Save file
        var fileId = request.Message.Document.FileId;
        // var filePath = await _storage.SaveDocumentAsync(fileId, user.TelegramUserId, "passport");
        
        request.User.CurrentState = UserState.WaitingForVehicleDoc;
        
        await userRepository.SaveChangesAsync();

        await botClient.SendMessage(request.Message.Chat.Id, "✅ Passport received. Now, please upload your vehicle registration certificate.", cancellationToken: cancellationToken);
    }
}
