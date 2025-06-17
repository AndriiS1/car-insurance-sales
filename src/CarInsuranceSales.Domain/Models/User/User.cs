namespace CarInsuranceSales.Domain.Models.User;

public class User
{
    public required Guid Id { get; init; }
    public required long ExternalUserId { get; init; }
    public required UserState CurrentState { get; set; }
    public required DateTime Created { get; init; }
}
