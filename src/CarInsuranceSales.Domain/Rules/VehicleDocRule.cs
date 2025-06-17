namespace CarInsuranceSales.Domain.Rules;

public static class VehicleDocRule
{
    public static string GetVehicleDocPath(Guid userId, Guid conversationId, string extension) => $"{userId}/{conversationId}/vehicle_document{extension}";
}
