using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Rewards;

internal sealed class ItemRewardJsonConverter : JsonConverter<ItemReward>
{
    public const string DiscriminatorValue = "item_reward";

    public override ItemReward Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ItemReward value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ItemReward Read(JsonElement json)
    {
        return new ItemReward
        {
            Id = json.GetProperty("id").GetInt32(),
            Count = json.GetProperty("count").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, ItemReward value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementRewardJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("count", value.Count);
        writer.WriteEndObject();
    }
}
