using System.ComponentModel.DataAnnotations;
namespace CarInsuranceSales.UseCases.Services.TelegramBot.Options;

public class TelegramBotOptions
{
    public const string Section = "TelegramBot";
    [Required]
    public required string ApiToken { get; init; }
}