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
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
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

    public static CollectionAchievement Read(in JsonElement json)
    {
        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new CollectionAchievement
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString, UriKind.RelativeOrAbsolute) : null,
            Description = json.GetProperty("description").GetStringRequired(),
            Requirement = json.GetProperty("requirement").GetStringRequired(),
            LockedText = json.GetProperty("locked_text").GetStringRequired(),
            Flags = AchievementFlagsJsonConverter.Read(json.GetProperty("flags")),
            Tiers = json.GetProperty("tiers").GetList(AchievementTierJsonConverter.Read),
            Rewards = json.GetProperty("rewards")
                .GetNullableList(AchievementRewardJsonConverter.Read),
            Bits = json.GetProperty("bits").GetNullableList(AchievementBitJsonConverter.Read),
            Prerequisites =
                json.GetProperty("prerequisites").GetList(static (in prerequisite) => prerequisite.GetInt32()),
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
