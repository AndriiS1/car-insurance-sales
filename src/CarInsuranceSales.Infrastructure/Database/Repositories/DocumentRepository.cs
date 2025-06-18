using CarInsuranceSales.Domain.Models.Document;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database.Repositories;

public class DocumentRepository(AppDbContext context) : IDocumentRepository
{
    private readonly DbSet<Document> _dbSet = context.Set<Document>();
    
    public async Task Upsert(Document document)
    {
        var existing = await _dbSet.FindAsync(document.Id);
        
        if (existing == null)
        {
            _dbSet.Add(document);
        }
        else
        {
            _dbSet.Entry(existing).CurrentValues.SetValues(document);
        }
    }
    
    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}
