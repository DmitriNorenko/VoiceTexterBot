﻿using System;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace VoiceTexterBot
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var host = new HostBuilder().
               ConfigureServices((hostContext, services) =>
               ConfigureServices(services)).UseConsoleLifetime().Build();

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();

            Console.WriteLine("Сервис остановлен");
        }
        static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITelegramBotClient>(provider =>
            new TelegramBotClient("6669545490:AAHl6TbKqjgYXXagHiJPCBrSHG2R_3KyXpQ"));
            services.AddHostedService<Bot>();
        }
    }
}