using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Commands.ConfirmOCR;

public sealed record ConfirmOCRCommand(Message Message, User User) : IRequest;
