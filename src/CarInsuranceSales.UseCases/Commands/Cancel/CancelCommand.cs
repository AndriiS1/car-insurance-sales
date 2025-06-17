using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.Cancel;

public sealed record CancelCommand(Message Message, User User) : IRequest;