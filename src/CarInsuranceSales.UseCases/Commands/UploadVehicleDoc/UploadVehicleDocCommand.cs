using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.UploadVehicleDoc;

public sealed record UploadVehicleDocCommand(Message Message, User User) : IRequest;