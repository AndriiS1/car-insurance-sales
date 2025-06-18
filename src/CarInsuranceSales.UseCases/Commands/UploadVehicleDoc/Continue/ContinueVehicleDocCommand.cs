using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc.Continue;

public sealed record ContinueVehicleDocCommand(CallbackQuery Callback, User User) : IRequest;