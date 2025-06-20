namespace CarInsuranceSales.Infrastructure.Services.PassportService.Dtos;

public class PassportResponse
{
    public required string IdValue { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string BirthDate  { get; set; }
    public required string BirthPlace { get; set; }
    public required string IssuanceDate { get; set; }
    public required string ExpiryDate { get; set; }
    public required string Country {get;set;}
    public required string Gender { get; set; }
    public required string Mrz1 { get; set; }
    public required string Mrz2 { get; set; }
}
