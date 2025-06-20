using CarInsuranceSales.Domain.Models.Document;
namespace CarInsuranceSales.Infrastructure.Services.FileService;

public interface IFileService
{
    Task SaveFile(Document document, CancellationToken cancellationToken);
}
