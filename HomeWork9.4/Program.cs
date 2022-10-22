using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;

namespace HomeWork9._4
{
    class Program
    {
        static TelegramBotClient botClient;
        
        
        static void Main(string[] args)
        {
            AppContext db = new AppContext();
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Thread task = new Thread(BotClientRun);
            task.Start();
            Console.ReadLine();
        }

        static async void BotClientRun()
        {
            string token = System.IO.File.ReadAllText(@"C:\Users\User\source\repos\TG_bots\mytemptestbot2022_bot_token.txt");

            botClient = new TelegramBotClient(token);

            CancellationTokenSource cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            botClient.StartReceiving(Handlers.HandleUpdateAsync, Handlers.HandleErrorAsync, receiverOptions, cancellationToken: cts.Token);

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();

            
        }

        
        //private static void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        //{
        //    string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

        //    Console.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");


        //    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document)
        //    {
        //        Console.WriteLine(e.Message.Document.FileId);
        //        Console.WriteLine(e.Message.Document.FileName);
        //        Console.WriteLine(e.Message.Document.FileSize);

        //        DownLoad(e.Message.Document.FileId, e.Message.Document.FileName);
        //    }

        //    if (e.Message.Text == null) return;

        //    var messageText = e.Message.Text;


        //    bot.SendTextMessageAsync(e.Message.Chat.Id,
        //        $"{messageText}"
        //        );
        //}

        //static async void DownLoad(string fileId, string path)
        //{
        //    var file = await bot.GetFileAsync(fileId);
        //    FileStream fs = new FileStream("_" + path, FileMode.Create);
        //    await bot.DownloadFileAsync(file.FilePath, fs);
        //    fs.Close();

        //    fs.Dispose();
        //}


    }
}
        
