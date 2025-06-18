using CarInsuranceSales.Domain.Models.User;
using CarInsuranceSales.UseCases.Commands.Cancel;
using CarInsuranceSales.UseCases.Commands.ConfirmOCR;
using CarInsuranceSales.UseCases.Commands.ConfirmPrice;
using CarInsuranceSales.UseCases.Commands.Start;
using CarInsuranceSales.UseCases.Commands.UploadPassport;
using CarInsuranceSales.UseCases.Commands.UploadPassport.Continue;
using CarInsuranceSales.UseCases.Commands.UploadPassport.Reupload;
using CarInsuranceSales.UseCases.Commands.UploadVehicleDoc;
using CarInsuranceSales.UseCases.Commands.UploadVehicleDoc.Continue;
using CarInsuranceSales.UseCases.Commands.UploadVehicleDoc.Reupload;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = CarInsuranceSales.Domain.Models.User.User;
namespace CarInsuranceSales.UseCases.Services.CommandProcessor;

public class CommandProcessor(IMediator mediator, IUserRepository userRepository) : ICommandProcessor
{
    private async Task<IRequest?> GetCommandRequest(Update update)
    {
        var userId = GetUserId(update);
        var user = await userRepository.GetByExternalUserId(userId);
        
        if (user is null)
        {
            var newUser = new User
            {
                CurrentState = UserState.WaitingForPassport,
                ExternalUserId = userId,
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
            };
            
            await userRepository.Create(newUser);
            await userRepository.SaveChangesAsync();
            
            user = newUser;
        }
        
        if (update.CallbackQuery is { } callback)
        {
            var data = callback.Data;
            
            switch (data)
            {
                case "doc:continue":
                    return new ContinuePassportCommand(callback, user);
                case "doc:reupload":
                    return new ReuploadPassportCommand(callback, user);
                case "vehicle_doc:continue":
                    return new ContinueVehicleDocCommand(callback, user);
                case "vehicle_doc:reupload":
                    return new ReuploadVehicleDocCommand(callback, user);
            }
        }

        var message = update.Message;

        if (message is null) return null;
        
        return message.Text switch
        {
            "/start" => new StartCommand(message, user),
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

    private static long GetUserId(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message        => update.Message?.From?.Id,
            UpdateType.CallbackQuery  => update.CallbackQuery?.From.Id,
            UpdateType.EditedMessage  => update.EditedMessage?.From?.Id,
            UpdateType.ChannelPost    => update.ChannelPost?.From?.Id,
            UpdateType.MyChatMember   => update.MyChatMember?.From.Id,
            _ => null
        } ?? throw new InvalidOperationException("Could not determine Telegram user ID");
    }
}
