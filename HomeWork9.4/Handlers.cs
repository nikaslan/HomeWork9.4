using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace HomeWork9._4
{
    public class Handlers
    {
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                _ => UnknownUpdateHandlerAsync(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            Console.WriteLine($"Receive message type: {message.Type}");
            var chatId = message.Chat.Id;
            var messageText = message.Text;

            if (message.Type == MessageType.Text)
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You said:\n" + messageText);
                return;
            }
            else if (message.Type == MessageType.Document)
            {
                DownloadFile(botClient, message.Document.FileId, message.Document.FileName);

                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "File was succesfully saved");
                return;
            }
            else if (message.Type == MessageType.Photo)
            {
                
                DownloadFile(botClient, message.Photo[3].FileId, DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")+".jpg");
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "This bot doesn't work with photos yet");
                return;
            }
            else if (message.Type == MessageType.Audio)
            {
                DownloadFile(botClient, message.Audio.FileId, message.Audio.FileName);
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "This bot doesn't work with audio yet");
                return;
            }
            else if (message.Type == MessageType.Voice)
            {
                
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "This bot doesn't work with voice messages yet");
                return;
            }          

        }

        static async void DownloadFile(ITelegramBotClient botClient, string fileId, string path)
        {
            var file = await botClient.GetFileAsync(fileId);
            FileStream fs = new FileStream("C:\\Files\\" + path, FileMode.Create);
            await botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }

        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// тут будет метод записи информации о файле в базу данных
        /// </summary>
        static void AddFileToDatabase()
        {
            
        }
    }
}
