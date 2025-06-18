using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport.Reupload;

public sealed record ReuploadPassportCommand(CallbackQuery Callback, User User) : IRequest;