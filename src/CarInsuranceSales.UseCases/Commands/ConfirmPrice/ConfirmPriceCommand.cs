using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.ConfirmPrice;

public sealed record ConfirmPriceCommand(Message Message, User User) : IRequest;