
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
namespace PoverkaEZ.Data;
public class TelegramBotService : IHostedService
{
    private readonly ITelegramBotClient _botClient;

    public TelegramBotService()
    {
        _botClient = new TelegramBotClient("7288606746:AAFnxmZKHg2ArY7sKmXuFF1wIL-NzyZfaJc");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _botClient.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
            cancellationToken: cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {

        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;
            var username = message.From.Username;
            var userId = message.From.Id;

            if (message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Здравствуйте, это бот компании PoverkaKZN, он предназначен для того чтобы вам было удобно получать информацию о вашей заявке!");

                // Сохранение chatId пользователя
                SaveChatId(username, userId);

                return;
            }
            await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Handle errors here
        return Task.CompletedTask;
    }

    
    private void SaveChatId(string username, long chatId)
    {
        try
        {
            MD mD = new MD();
            User user = mD.FindUserByTelegram(username);
            user.chatid = chatId;
            mD.ReplaceUser(user.Username, user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении chatId: {ex.Message}");
        }
    }
    public async Task SendMessageToChatIdAsync(long chatId, string message)
    {
        try
        {
            await _botClient.SendTextMessageAsync(chatId, message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке сообщения: {ex.Message}");
        }
    }
}
