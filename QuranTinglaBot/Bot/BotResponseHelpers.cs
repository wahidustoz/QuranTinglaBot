using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace QuranTinglaBot.Bot
{
    public class BotResponseHelpers
    {
        public static InlineKeyboardMarkup GetInlineKeyboard()
        {
            var buttons = new List<InlineKeyboardButton[]>();

            var markup = new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Suralar", "surahs"),
                    InlineKeyboardButton.WithCallbackData("Oyatlar", "ayahs")
                }
            });

            return markup;
        }
    }
}