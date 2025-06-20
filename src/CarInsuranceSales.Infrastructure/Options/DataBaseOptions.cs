using System.ComponentModel.DataAnnotations;
namespace CarInsuranceSales.Infrastructure.Options;

public class DataBaseOptions
{
    public const string Section = "DataBase";
    
    [Required]
    public required string ConnectionString { get; init; }
}