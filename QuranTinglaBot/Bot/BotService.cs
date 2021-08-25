using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuranTinglaBot.ApiClients;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace QuranTinglaBot.Bot
{
    public class BotService : BackgroundService
    {
        private readonly OyatClient mOyatClient;
        private readonly ILogger<BotService> mLogger;
        private readonly UpdateHandler mUpdateHandler;
        private TelegramBotClient mBot;
        private User mMe;

        public BotService(
                ILogger<BotService> logger, 
                OyatClient oyatClient,
                UpdateHandler updateHandler)
        {
            mOyatClient = oyatClient;
            mLogger = logger;
            mUpdateHandler = updateHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            mBot = new TelegramBotClient(Constants.BotApi);
            mMe = await mBot.GetMeAsync();

            mLogger.LogInformation($"{mMe.Username.ToUpper()} started successfully.");
            
            using var cts = new CancellationTokenSource();
            mBot.StartReceiving(new DefaultUpdateHandler(mUpdateHandler.HandleUpdateAsync, mUpdateHandler.HandleErrorAsync),
                               cts.Token);
            
            Console.ReadLine();
        }
    }
}