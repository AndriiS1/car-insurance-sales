using CarInsuranceSales.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<User> _dbSet = context.Set<User>();
    
    public async Task Create(User user)
    {
        await _dbSet.AddAsync(user);
    }
    
    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
