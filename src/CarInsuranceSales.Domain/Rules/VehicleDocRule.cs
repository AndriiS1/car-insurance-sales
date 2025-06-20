namespace CarInsuranceSales.Domain.Rules;

public static class VehicleDocRule
{
    public static string GetVehicleDocPath(Guid userId, Guid conversationId, string extension) => $"Storage/Documents/{userId}/{conversationId}/vehicle_document{extension}";
}
