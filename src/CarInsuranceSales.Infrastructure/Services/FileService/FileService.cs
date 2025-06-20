using CarInsuranceSales.Domain.Models.Document;
using Telegram.Bot;
namespace CarInsuranceSales.Infrastructure.Services.FileService;

public class FileService(ITelegramBotClient botClient) : IFileService
{
    public async Task SaveFile(Document document, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(document.FilePath)!);
        
        await using var fileStream = File.Create(document.FilePath);
        
        await botClient.DownloadFile(document.ExternalFilePath, fileStream, cancellationToken);
    }
}
