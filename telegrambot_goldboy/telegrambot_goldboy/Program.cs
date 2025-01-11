using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{
    private static ITelegramBotClient botClient = new TelegramBotClient("7845781687:AAFCT8XzM8ZhKQkZJT2pKbtUERvuKn2VETc");  // Tokenni shu yerga yozing

    static async Task Main(string[] args)
    {
        Console.WriteLine("Bot ishga tushdi...");

        botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync);

        Console.WriteLine("Bot ishlamoqda. Xabarlarni kutyapman...");
        Console.ReadLine();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message && update.Message?.Text != null)
        {
            var message = update.Message;
            Console.WriteLine($"Xabar keldi: {message.Text} | Foydalanuvchi: {message.Chat.Id}");

            string filePath = "data.json"; // JSON fayl yo‘li

            if (message.Text == "/start")
            {
                string response = "Assalomu alaykum! Botga xush kelibsiz.\nDo'stlaringizni taklif qiling va mukofot oling!";
                await botClient.SendTextMessageAsync(message.Chat.Id, response);

                // Foydalanuvchini JSON faylga saqlash
                User newUser = new User
                {
                    ChatId = message.Chat.Id,
                    Username = message.Chat.Username ?? "Noma'lum"
                };
            }
        }
    }


    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Xatolik: {exception.Message}");
        return Task.CompletedTask;
    }
}
