﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace VoiceTexterBot.Controllers
{
    internal class InlineMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public InlineMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} обнаружил нажатие на кнопку");
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"Обнаружено нажатие на кнопку", cancellationToken: ct);
        }
    }
}
