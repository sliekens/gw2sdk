using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Rewards;

internal sealed class CoinsRewardJsonConverter : JsonConverter<CoinsReward>
{
    public const string DiscriminatorValue = "coin_reward";

    public override CoinsReward Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CoinsReward value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static CoinsReward Read(in JsonElement json)
    {
        return new CoinsReward { Coins = json.GetProperty("coins").GetInt32() };
    }

    public static void Write(Utf8JsonWriter writer, CoinsReward value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementRewardJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("coins", value.Coins);
        writer.WriteEndObject();
    }
}
