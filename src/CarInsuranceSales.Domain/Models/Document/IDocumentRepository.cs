namespace CarInsuranceSales.Domain.Models.Document;

public interface IDocumentRepository
{
    Task Upsert(Document document);
    Task SaveChangesAsync();
}
