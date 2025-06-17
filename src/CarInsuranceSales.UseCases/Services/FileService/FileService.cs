using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.User;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Services.FileService;

public class FileService(ITelegramBotClient botClient) : IFileService
{
    public async Task SaveFile(Document document, User user, CancellationToken cancellationToken)
    {
        var savePath = Path.Combine("Storage", "Documents", document.FilePath);
        
        Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
        
        await using var fileStream = File.Create(savePath);
        
        await botClient.DownloadFile(document.ExternalFilePath, fileStream, cancellationToken);
    }
}
