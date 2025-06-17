using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.Start;

public sealed record StartCommand(Message Message, User User) : IRequest;