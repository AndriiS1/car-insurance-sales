using MediatR;
using Telegram.Bot.Types;
namespace CarInsuranceSales.UseCases.Commands.Start;

public sealed record StartCommand(Message Message) : IRequest;