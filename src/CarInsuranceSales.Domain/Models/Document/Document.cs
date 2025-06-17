namespace CarInsuranceSales.Domain.Models.Document;

public class Document
{
    public required Guid Id { get; init; }
    public required DocumentType Type { get; init; }
    public required string ExternalFileId { get; init; }
    public required string Extension { get; init; }
    public required Guid ConversationId { get; init; }
    public required string ExternalFilePath { get; init; }
    public required string FilePath { get; init; }
    public Conversation.Conversation Conversation { get; init; } = null!;
}
