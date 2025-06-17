namespace CarInsuranceSales.Domain.Models.User;

public interface IUserRepository
{
    Task Create(User user);
    Task<User?> GetByExternalUserId(long externalUserId);
    Task SaveChangesAsync();
}
