using MediatR;
using Telegram.Bot.Types;
namespace CarInsuranceSales.UseCases.Commands.Start;

public sealed record StartCommand(Update Update) : IRequest;