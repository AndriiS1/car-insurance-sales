using CarInsuranceSales.Domain.Models.User;
using CarInsuranceSales.UseCases.Commands.Cancel;
using CarInsuranceSales.UseCases.Commands.ConfirmOCR;
using CarInsuranceSales.UseCases.Commands.ConfirmPrice;
using CarInsuranceSales.UseCases.Commands.Start;
using CarInsuranceSales.UseCases.Commands.UploadPassport;
using CarInsuranceSales.UseCases.Commands.UploadVehicleDoc;
using MediatR;
using Telegram.Bot.Types;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Services;

public class CommandProcessor(IMediator mediator, IUserRepository userRepository) : ICommandProcessor
{
    private async Task<IRequest?> GetCommandRequest(Update update)
    {
        var message = update.Message;

        if (message == null) return null;

        var user = await userRepository.GetByExternalUserId(message.Chat.Id);
        
        if (user is null)
        {
            var newUser = new User
            {
                CurrentState = UserState.WaitingForPassport,
                ExternalUserId = message.Chat.Id,
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
            };
            
            await userRepository.Create(newUser);
            
            await userRepository.SaveChangesAsync();
            
            user = newUser;
        }

        return message.Text switch
        {
            "/start" => new StartCommand(message),
            "/cancel" => new CancelCommand(message, user),
            _ => user.CurrentState switch
            {
                UserState.WaitingForPassport => new UploadPassportCommand(message, user),
                UserState.WaitingForVehicleDoc => new UploadVehicleDocCommand(message, user),
                UserState.ConfirmingOCR => new ConfirmOCRCommand(message, user),
                UserState.ConfirmingPrice => new ConfirmPriceCommand(message, user),
                _ => null
            }
        };
    }
    
    public async Task Process(Update update)
    {
        var request = await GetCommandRequest(update);

        if (request != null)
        {
            await mediator.Send(request);
        }
    }
}
