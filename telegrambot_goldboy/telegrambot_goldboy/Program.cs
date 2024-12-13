using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class Program
{
    private static TelegramBotClient TelegramBot;
    private const string startCommand = "/start";
    private const string businesOwner = "Biznes egasi";
    private const string istemolchi = "Istemolchi";
    private static void Main(string[] args)
    {
        string token = @"7845781687:AAFCT8XzM8ZhKQkZJT2pKbtUERvuKn2VETc";
        TelegramBot = new TelegramBotClient(token);
        
        TelegramBot.StartReceiving(HandlelUpdate, HandlelError);

        Console.ReadLine();
    }

    private static async Task HandlelError(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        await client.SendTextMessageAsync(
            chatId: 1004979323,
            $"Error: {exception.Message}");
    }

    private static async Task HandlelUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (true)
        {
            if (update.Message.Text is startCommand)
            {
                var markup = menuMarkUp();

                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Welcome",
                    replyMarkup: markup);

                await client.SendTextMessageAsync(
                chatId: 1004979323,
                $"foydalanuvchi malumotlari:\n" +
                $"Ism: {update.Message.Chat.FirstName}\n" +
                $"Familiya: {update.Message.Chat.LastName ?? "Familiya kiritilmagan"}\n" +
                $"Chat ID: {update.Message.Chat.Id}\n"+
                $"Username: @{update.Message.Chat.Username}" ?? "Username mavjud emas");
            }
            if (update.Message?.Text == "Biznes egasi")
            {
                // Telefon raqamini so'rash tugmasi
                var requestContactKeyboard = new ReplyKeyboardMarkup(new[]
                {
                new KeyboardButton("📱 Telefon raqamni yuborish") { RequestContact = true }
            })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };

                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Telefon raqamingizni yuborish uchun tugmani bosing:",
                    replyMarkup: requestContactKeyboard
                );
            }
            else if (update.Message?.Contact != null)
            {
                // Telefon raqami yuborilganda
                string phoneNumber = update.Message.Contact.PhoneNumber;

                await client.SendTextMessageAsync(
                    chatId: 1004979323,
                    text: $"Foydalanuvchi Telefon raqami: {phoneNumber}",
                    replyMarkup: new ReplyKeyboardRemove() // Tugmalarni olib tashlash
                );
            }
        }
        else
        {
            await client.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            $"Please send me only text.");
        }
    }

    private static ReplyKeyboardMarkup menuMarkUp()
    {
        return new ReplyKeyboardMarkup(new KeyboardButton[][]
        {new KeyboardButton[]{new KeyboardButton("Biznes egasi"), new KeyboardButton("Istemolchi") { RequestContact = true } } })
        { 
            ResizeKeyboard = true 
        };
    }
}