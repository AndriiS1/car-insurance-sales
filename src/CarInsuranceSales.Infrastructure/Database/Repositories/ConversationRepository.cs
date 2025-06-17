using CarInsuranceSales.Domain.Models.Conversation;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database.Repositories;

public class ConversationRepository(AppDbContext context) : IConversationRepository
{
    private readonly DbSet<Conversation> _dbSet = context.Set<Conversation>();
    
    public async Task Create(Conversation conversation)
    {
        await _dbSet.AddAsync(conversation);
    }

    public async Task<Conversation?> GetLastByUserId(Guid userId)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.Created)
            .FirstOrDefaultAsync();

    }
    
    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}
