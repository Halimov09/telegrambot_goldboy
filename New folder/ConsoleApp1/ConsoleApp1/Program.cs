using OpenAI.Chat;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("AI Chatbot: Bot bilan muloqot qilish uchun xabar yuboring. Chiqish uchun 'exit' deb yozing.\n");

            // OpenAI API kalitini shu yerga qo'ying
            string apiKey = "YOUR_OPENAI_API_KEY";
            var openAI = new OpenAIAPI(apiKey);

            while (true)
            {
                Console.Write("Siz: ");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "exit")
                {
                    Console.WriteLine("Chiqmoqda...");
                    break;
                }

                // OpenAI dan javob olish
                var chatRequest = new ChatRequest()
                {
                    Model = "gpt-3.5-turbo", // Model tanlash
                    Messages = new[]
                    {
                        new ChatMessage("user", userInput)
                    }
                };

                var chatResult = await openAI.Chat.CreateChatCompletionAsync(chatRequest);
                string botResponse = chatResult.Choices[0].Message.Content;

                Console.WriteLine($"Bot: {botResponse}\n");
            }
        }
    }
}
