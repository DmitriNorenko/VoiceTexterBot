using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace VoiceTexterBot
{
    internal class Bot
    {
        private ITelegramBotClient _telegramClient;
        public Bot(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cansellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await
                    _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы нажали кнопку",
                    cancellationToken: cansellationToken);
                return;
            }
            if (update.Type == UpdateType.Message)
            {
                await
                    _telegramClient.SendTextMessageAsync(update.Message.Chat.Id,
                    "Вы отправили сообщение", cancellationToken: cansellationToken);
                return;
            }
        }
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error: \n[{apiRequestException.ErrorCode}]\n" +
                $"{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);
            return Task.CompletedTask;
        }
    }
}
