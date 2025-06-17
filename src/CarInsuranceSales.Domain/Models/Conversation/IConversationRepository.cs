namespace CarInsuranceSales.Domain.Models.Conversation;

public interface IConversationRepository
{
    Task Create(Conversation conversation);
    Task<Conversation?> GetLastByUserId(Guid userId);
    Task SaveChangesAsync();
}
