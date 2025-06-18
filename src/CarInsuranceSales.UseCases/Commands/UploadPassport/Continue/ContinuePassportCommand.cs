using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport.Continue;

public sealed record ContinuePassportCommand(CallbackQuery Callback, User User) : IRequest;