using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Rewards;

internal sealed class TitleRewardJsonConverter : JsonConverter<TitleReward>
{
    public const string DiscriminatorValue = "title_reward";

    public override TitleReward Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, TitleReward value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static TitleReward Read(JsonElement json)
    {
        return new TitleReward
        {
            Id = json.GetProperty("id").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, TitleReward value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementRewardJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteEndObject();
    }
}
