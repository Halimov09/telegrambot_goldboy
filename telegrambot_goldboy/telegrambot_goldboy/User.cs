using System.Text.Json;

public partial class User
{
    public long ChatId { get; set; }
    public string Username { get; set; }
};

public static partial void SaveUserToFile(User user, string filePath)
{

}
