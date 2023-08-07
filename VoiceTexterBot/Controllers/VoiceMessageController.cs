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
using VoiceTexterBot.Services;

namespace VoiceTexterBot.Controllers
{
    public class VoiceMessageController
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(ITelegramBotClient telegramBotClient,
            AppSettings appSettings, IFileHandler audioFileHandler)
        {
            _appSettings = appSettings;
            _audioFileHandler = audioFileHandler;
            _telegramClient = telegramBotClient;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null) return;

            await _audioFileHandler.Download(fileId, ct);

            await
                _telegramClient.SendTextMessageAsync(message.Chat.Id,
                "Голосовое сообщение загружено", cancellationToken: ct);
        }
    }
}
