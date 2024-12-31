using System.Text.Json;

public partial class User
{
    public long ChatId { get; set; }
    public string Username { get; set; }
};

public static partial void SaveUserToFile(User user, string filePath)
{
    List<User> users = new();

    // Fayl mavjud bo‘lsa, mavjud ma'lumotlarni o‘qish
    if (File.Exists(filePath))
    {
        string existingData = File.ReadAllText(filePath);
        if (!string.IsNullOrWhiteSpace(existingData))
        {
            users = JsonSerializer.Deserialize<List<User>>(existingData);
        }
    }

    // Yangi foydalanuvchini ro‘yxatga qo‘shish
    users.Add(user);

    // Yangilangan ro‘yxatni JSON faylga yozish
    string jsonData = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(filePath, jsonData);
}
