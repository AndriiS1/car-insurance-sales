namespace CarInsuranceSales.Domain.Rules;

public static class PassportRule
{
    public static string GetVehicleDocPath(Guid userId, Guid conversationId, string extension) => $"{userId}/{conversationId}/passport{extension}";
}
