using CarInsuranceSales.Domain.Models;
using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Domain.Models.ExtractedField;
using CarInsuranceSales.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
namespace CarInsuranceSales.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Document> Document { get; set; }
    public DbSet<ExtractedField> ExtractedFields { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Error> Errors { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<PolicyEvent> PolicyEvents { get; set; }
}