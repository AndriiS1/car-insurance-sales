using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc.Reupload;

public sealed record ReuploadVehicleDocCommand(CallbackQuery Callback, User User) : IRequest;