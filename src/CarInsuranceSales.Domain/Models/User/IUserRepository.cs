namespace CarInsuranceSales.Domain.Models.User;

public interface IUserRepository
{
    Task Create(User user);
    Task SaveChangesAsync();
}
