using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceTexterBot.Configuration;

namespace VoiceTexterBot.Controllers
{
    public class VoiceMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public VoiceMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Русский" , $"ru"),
                        InlineKeyboardButton.WithCallbackData($"English" , $"en")
                    });
                    await
                        _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>" +
                        $"Наш бот превращает аудио в текст.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Можно записать сообщение и переслать другу," +
                        $" если лень писать.{Environment.NewLine}", cancellationToken: ct,
                        parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    await
                        _telegramClient.SendTextMessageAsync(message.Chat.Id,
                        "Отправьте аудио для превращения в текст.", cancellationToken: ct);
                    break;
            }
        }
    }
}
