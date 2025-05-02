using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal sealed class CollectionAchievementJsonConverter : JsonConverter<CollectionAchievement>
{
    public const string DiscriminatorValue = "collection_achievement";

    public override CollectionAchievement Read(
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
        CollectionAchievement value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static CollectionAchievement Read(JsonElement json)
    {
        return new CollectionAchievement
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            IconHref = json.GetProperty("icon").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Requirement = json.GetProperty("requirement").GetStringRequired(),
            LockedText = json.GetProperty("locked_text").GetStringRequired(),
            Flags = AchievementFlagsJsonConverter.Read(json.GetProperty("flags")),
            Tiers = json.GetProperty("tiers").GetList(AchievementTierJsonConverter.Read),
            Rewards = json.GetProperty("rewards")
                .GetNullableList(AchievementRewardJsonConverter.Read),
            Bits = json.GetProperty("bits").GetNullableList(AchievementBitJsonConverter.Read),
            Prerequisites =
                json.GetProperty("prerequisites").GetList(prerequisite => prerequisite.GetInt32()),
            PointCap = json.GetProperty("point_cap").GetNullableInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, CollectionAchievement value)
    {
        writer.WriteStartObject();
        writer.WriteString(AchievementJsonConverter.DiscriminatorName, DiscriminatorValue);
        AchievementJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
