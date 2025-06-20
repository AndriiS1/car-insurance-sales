using MediatR;
namespace CarInsuranceSales.UseCases.Commands.ConfirmPrice;

public class ConfirmPriceCommandHandler : IRequestHandler<ConfirmPriceCommand>
{
    public async Task Handle(ConfirmPriceCommand request, CancellationToken cancellationToken)
    {
    }
}
