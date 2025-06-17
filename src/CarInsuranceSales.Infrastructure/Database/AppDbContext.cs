using CarInsuranceSales.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}