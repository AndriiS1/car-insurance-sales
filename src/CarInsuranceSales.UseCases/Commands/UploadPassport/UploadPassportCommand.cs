using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.UploadPassport;

public sealed record UploadPassportCommand(Message Message, User User) : IRequest;