using CarInsuranceSales.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly DbSet<User> _dbSet = context.Set<User>();
    
    public async Task Create(User user)
    {
        await _dbSet.AddAsync(user);
    }
    
    public async Task<User?> GetByExternalUserId(long externalUserId)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.ExternalUserId == externalUserId);
    }
    
    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}
