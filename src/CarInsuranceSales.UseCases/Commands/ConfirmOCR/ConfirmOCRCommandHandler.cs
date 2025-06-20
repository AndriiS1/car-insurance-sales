using MediatR;
namespace CarInsuranceSales.UseCases.Commands.ConfirmOCR;

public class ConfirmOCRCommandHandler : IRequestHandler<ConfirmOCRCommand>
{
    public async Task Handle(ConfirmOCRCommand request, CancellationToken cancellationToken)
    {
    }
}
