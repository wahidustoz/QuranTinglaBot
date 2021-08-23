using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuranTinglaBot.ApiClients;
using QuranTinglaBot.Entity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuranTinglaBot.Bot
{
    public class UpdateHandler
    {
        private readonly ILogger<UpdateHandler> mLogger;
        private readonly OyatClient mOyatClient;
        private readonly AppDbContext mContext;

        public UpdateHandler(
                ILogger<UpdateHandler> logger,
                OyatClient oyatClient,
                AppDbContext dbContext)
        {
            mLogger = logger;
            mOyatClient = oyatClient;
            mContext = dbContext;
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch(update.Type)
            {
                case UpdateType.Message: await BotOnMessageReceived(botClient, update.Message); break;
                default: await BotOnUnknowUpdateReceived(botClient, update); break;
            }
        }

        private Task BotOnUnknowUpdateReceived(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        private async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            if(message.Type == MessageType.Audio)
            {
                mLogger.LogInformation($"AUDIO: {JsonConvert.SerializeObject(message)}");
                if(await mContext.Admins.AnyAsync(a => a.UserId == message.Chat.Id.ToString()))
                {
                    if(int.TryParse(message.Caption, out var surahNum))
                    {
                        if(await mContext.Surahs.AnyAsync(s => s.Number == surahNum))
                        {
                            await botClient.SendTextMessageAsync(chatId: message.Chat.Id, $"{surahNum} raqamli sura allaqachon databazada bor!");
                            return;
                        }

                        if(surahNum >= 1 && surahNum <= 114)
                        {
                            var surah = new Surah()
                            {
                                MessageId = message.MessageId.ToString(),
                                Number = surahNum
                            };

                            mContext.Surahs.Add(surah);
                            await mContext.SaveChangesAsync();

                            var msg = $"{surahNum} raqamli sura Databazaga qo'shildi!";
                            mLogger.LogInformation(msg);
                            await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: msg);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(chatId: message.Chat.Id, $"{surahNum} raqamli sura mavjud emas!");
                            return;
                        }
                    }

                    return;
                }

            }

            if(string.IsNullOrEmpty(message.Text))
            {
                return;
            }

            if(message.Text == Constants.AdminSecret)
            {
                var admin = new Admin()
                {
                    UserId = message.Chat.Id.ToString(),
                    Username = message.Chat.Username,
                    FullName = $"{message.Chat.FirstName} {message.Chat.LastName}"
                };


                if(await mContext.Admins.AnyAsync(a => a.UserId == admin.UserId))
                {
                    mLogger.LogInformation($"Admin exists, NOT added: {JsonConvert.SerializeObject(admin, Formatting.Indented)}");
                    await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Siz allaqachon kuchaygansiz 😎");
                    return;
                }

                mContext.Admins.Add(admin);
                await mContext.SaveChangesAsync();
                mLogger.LogInformation($"Admin added: {JsonConvert.SerializeObject(admin, Formatting.Indented)}");

                await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Congratulations! \nSiz ham kuchaydingiz 😎");
            }

            if(message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Choose",
                                                            replyMarkup: BotResponseHelpers.GetInlineKeyboard());
            }
        }
    }
}