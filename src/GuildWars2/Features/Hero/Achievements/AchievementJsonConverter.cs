using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal sealed class AchievementJsonConverter : JsonConverter<Achievement>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "achievement";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Achievement).IsAssignableFrom(typeToConvert);
    }

    public override Achievement Read(
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
        Achievement value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static Achievement Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case CollectionAchievementJsonConverter.DiscriminatorValue:
                    return CollectionAchievementJsonConverter.Read(json);
                default:
                    break;
            }
        }

        string iconString = json.GetProperty("icon").GetString() ?? "";
        return new Achievement
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
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

    public static void Write(Utf8JsonWriter writer, Achievement value)
    {
        switch (value)
        {
            case CollectionAchievement collectionAchievement:
                CollectionAchievementJsonConverter.Write(writer, collectionAchievement);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, Achievement value)
    {
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("icon", value.IconUrl?.ToString());
        writer.WriteString("description", value.Description);
        writer.WriteString("requirement", value.Requirement);
        writer.WriteString("locked_text", value.LockedText);
        writer.WritePropertyName("flags");
        AchievementFlagsJsonConverter.Write(writer, value.Flags);
        writer.WritePropertyName("tiers");
        writer.WriteStartArray();
        foreach (AchievementTier? tier in value.Tiers)
        {
            AchievementTierJsonConverter.Write(writer, tier);
        }

        writer.WriteEndArray();
        writer.WritePropertyName("rewards");
        if (value.Rewards is not null)
        {
            writer.WriteStartArray();
            foreach (AchievementReward? reward in value.Rewards)
            {
                AchievementRewardJsonConverter.Write(writer, reward);
            }

            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("bits");
        if (value.Bits is not null)
        {
            writer.WriteStartArray();
            foreach (AchievementBit? bit in value.Bits)
            {
                AchievementBitJsonConverter.Write(writer, bit);
            }

            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("prerequisites");
        writer.WriteStartArray();
        foreach (int prerequisite in value.Prerequisites)
        {
            writer.WriteNumberValue(prerequisite);
        }

        writer.WriteEndArray();

        writer.WritePropertyName("point_cap");
        if (value.PointCap.HasValue)
        {
            writer.WriteNumberValue(value.PointCap.Value);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
