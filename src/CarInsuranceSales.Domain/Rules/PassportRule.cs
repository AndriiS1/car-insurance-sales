namespace CarInsuranceSales.Domain.Rules;

public static class PassportRule
{
    public static string GetVehicleDocPath(Guid userId, Guid conversationId, string extension) => $"Storage/Documents/{userId}/{conversationId}/passport{extension}";
}
