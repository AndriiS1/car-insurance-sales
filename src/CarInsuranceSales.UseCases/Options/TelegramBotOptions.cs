using System.ComponentModel.DataAnnotations;
namespace CarInsuranceSales.UseCases.Options;

public class TelegramBotOptions
{
    public const string Section = "TelegramBot";
    
    [Required]
    public required string ApiToken { get; init; }
    
    [Required]
    public required string WebhookUrl { get; init; }
}