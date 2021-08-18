using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuranTinglaBot.Bot
{
    public class UpdateHandler
    {
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch(update.Type)
            {
                case UpdateType.Message: await BotOnMessageReceived(botClient, update.Message); break;
                default: await BotOnUnknowUpdateReceived(botClient, update); break;
            }
        }

        private static Task BotOnUnknowUpdateReceived(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        private static async Task<Message> BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            System.Console.WriteLine($"{message.Chat.Id} said: {message.Text}");

            if(message.Text == "/start")
            {
                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Choose",
                                                            replyMarkup: BotResponseHelpers.GetInlineKeyboard());
            }

            return new Message();
        }
    }
}