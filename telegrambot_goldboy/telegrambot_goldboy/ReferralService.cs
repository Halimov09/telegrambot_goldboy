using System.Text.Json;
using telegrambot_goldboy;

public class ReferralService
{
    private string dataFilePath = "data.json";  // JSON fayl nomi

    // JSON faylni o'qish
    public Dictionary<string, UserReferral> LoadData()
    {
        if (!File.Exists(dataFilePath)) return new Dictionary<string, UserReferral>();

        var json = File.ReadAllText(dataFilePath);
        return JsonSerializer.Deserialize<Dictionary<string, UserReferral>>(json) ?? new Dictionary<string, UserReferral>();
    }

    // JSON faylga yozish
    public void SaveData(Dictionary<string, UserReferral> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataFilePath, json);
    }

    // Referral qo'shish
    public void HandleReferral(string userId, string referrerId)
    {
        var data = LoadData();

        if (!data.ContainsKey(referrerId))
        {
            data[referrerId] = new UserReferral { UserId = referrerId, ReferralsCount = 0 };
        }

        data[referrerId].ReferralsCount++;
        SaveData(data);
    }
}
