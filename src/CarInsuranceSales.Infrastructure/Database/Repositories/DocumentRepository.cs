using CarInsuranceSales.Domain.Models.Document;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database.Repositories;

public class DocumentRepository(AppDbContext context) : IDocumentRepository
{
    private readonly DbSet<Document> _dbSet = context.Set<Document>();
    
    public async Task Create(Document document)
    {
        await _dbSet.AddAsync(document);
    }
    
    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}
