using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceTexterBot.Services
{
    public interface IStorage
    {
        Session GetSessions(long chatId);
    }
}
