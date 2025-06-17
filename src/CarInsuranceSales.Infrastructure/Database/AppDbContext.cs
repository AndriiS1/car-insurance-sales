using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Document>  Documents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Conversations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        modelBuilder.Entity<Conversation>()
            .HasMany(e => e.Documents)
            .WithOne(e => e.Conversation)
            .HasForeignKey(e => e.ConversationId)
            .IsRequired();
    }
}