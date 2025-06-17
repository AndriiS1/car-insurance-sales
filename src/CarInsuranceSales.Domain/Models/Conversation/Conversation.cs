namespace CarInsuranceSales.Domain.Models.Conversation;

public class Conversation
{
    public required Guid Id { get; init; }
    public required DateTimeOffset Created { get; init; }
    public ICollection<Document.Document> Documents { get; } = new List<Document.Document>();
    public required Guid UserId { get; init; }
    public User.User User { get; init; } = null!;
}
