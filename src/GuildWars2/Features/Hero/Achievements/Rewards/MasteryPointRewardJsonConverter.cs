using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero.Masteries;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal sealed class MasteryPointRewardJsonConverter : JsonConverter<MasteryPointReward>
{
    public const string DiscriminatorValue = "mastery_point_reward";

    public override MasteryPointReward Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        MasteryPointReward value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static MasteryPointReward Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Region = json.GetProperty("region").GetEnum<MasteryRegionName>()
        };
    }

    public static void Write(Utf8JsonWriter writer, MasteryPointReward value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementRewardJsonConverter.DiscriminatorName, DiscriminatorValue);
        writer.WriteNumber("id", value.Id);
        writer.WriteString("region", value.Region.ToString());
        writer.WriteEndObject();
    }
}
