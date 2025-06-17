using CarInsuranceSales.Domain.Models.User;
using MediatR;
using Telegram.Bot;
namespace CarInsuranceSales.UseCases.Commands.Start;

public class StartCommandHandler(ITelegramBotClient botClient, IUserRepository userRepository) : IRequestHandler<StartCommand>
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

                                   Let's begin!
                                   """;
        
        await botClient.SendMessage(
            chatId: request.Message.Chat.Id,
            text: welcomeText,
            cancellationToken: cancellationToken
        );
    }
}
