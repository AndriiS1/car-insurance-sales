using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.User;
namespace CarInsuranceSales.UseCases.Services.FileService;

public interface IFileService
{
    Task SaveFile(Document document, User user, CancellationToken cancellationToken);
}
