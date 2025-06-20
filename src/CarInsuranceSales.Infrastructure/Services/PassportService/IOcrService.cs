using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Infrastructure.Services.PassportService.Dtos;
namespace CarInsuranceSales.Infrastructure.Services.PassportService;

public interface IOcrService
{
    Task<PassportResponse?> GetDocumentData(Document document);
}
