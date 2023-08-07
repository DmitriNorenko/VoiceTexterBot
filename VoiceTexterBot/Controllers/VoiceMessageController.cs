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
        private readonly IStorage _memoryStorage;
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(ITelegramBotClient telegramBotClient,
            AppSettings appSettings, IFileHandler audioFileHandler, IStorage memoryStorage)
        {
            _appSettings = appSettings;
            _audioFileHandler = audioFileHandler;
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _audioFileHandler.Download(fileId, ct);

            string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode; // Здесь получим язык из сессии пользователя
            var result = _audioFileHandler.Process(userLanguageCode); // Запустим обработку
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
        }
    }
}
