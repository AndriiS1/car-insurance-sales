using CarInsuranceSales.Domain.Models.Conversation;
using CarInsuranceSales.Domain.Models.User;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.Start;

public class StartCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository, IConversationRepository conversationRepository) : IRequestHandler<StartCommand>
{
    public async Task Handle(StartCommand request, CancellationToken cancellationToken)
    {
        const string welcomeText = """
                                   👋 Welcome to Car Insurance Assistant!

                                   I will guide you through the process of buying a car insurance policy. Here's how it works:

                                   1️⃣ Upload your passport and vehicle registration certificate  
                                   2️⃣ I will extract and show you the data from them  
                                   3️⃣ You confirm the data  
                                   4️⃣ The policy is generated and sent to you as a PDF  
                                   💵 Fixed Price: 100 USD

                                   Let's begin! Send your passport as a photo.
                                   """;
        
        var updateStateTask = SetUserStatus(request.User);
        var createConversationTask = CreateConversation(request.User.Id);

        await Task.WhenAll(updateStateTask, createConversationTask);
        
        await botClient.SendMessage(
            chatId: request.Message.Chat.Id,
            text: welcomeText,
            cancellationToken: cancellationToken
        );
    }
    
    private async Task SetUserStatus(User user)
    {
        user.CurrentState = UserState.WaitingForPassport;
        await userRepository.SaveChangesAsync();
    }

    private async Task CreateConversation(Guid userId)
    {
        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Created = DateTimeOffset.UtcNow
        };

        await conversationRepository.Create(conversation);
        await conversationRepository.SaveChangesAsync();
    }
}
