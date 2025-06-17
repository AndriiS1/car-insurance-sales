namespace CarInsuranceSales.Domain.Models.User;

public enum UserState
{
    Idle,
    WaitingForPassport,
    WaitingForVehicleDoc,
    ConfirmingOCR,
    ConfirmingPrice,
    GeneratingPolicy
}
