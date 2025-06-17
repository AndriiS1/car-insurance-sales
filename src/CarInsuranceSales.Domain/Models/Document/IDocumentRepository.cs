namespace CarInsuranceSales.Domain.Models.Document;

public interface IDocumentRepository
{
    Task Create(Document document);
    Task SaveChangesAsync();
}
