using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero.Achievements.Rewards;

namespace GuildWars2.Hero.Achievements;

internal sealed class AchievementRewardJsonConverter : JsonConverter<AchievementReward>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "reward";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(AchievementReward).IsAssignableFrom(typeToConvert);
    }

    public override AchievementReward Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, AchievementReward value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static AchievementReward Read(JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case CoinsRewardJsonConverter.DiscriminatorValue:
                    return CoinsRewardJsonConverter.Read(json);
                case ItemRewardJsonConverter.DiscriminatorValue:
                    return ItemRewardJsonConverter.Read(json);
                case MasteryPointRewardJsonConverter.DiscriminatorValue:
                    return MasteryPointRewardJsonConverter.Read(json);
                case TitleRewardJsonConverter.DiscriminatorValue:
                    return TitleRewardJsonConverter.Read(json);
            }
        }

        return new AchievementReward();
    }

    public static void Write(Utf8JsonWriter writer, AchievementReward value)
    {
        switch (value)
        {
            case CoinsReward coinsReward:
                CoinsRewardJsonConverter.Write(writer, coinsReward);
                break;
            case ItemReward itemReward:
                ItemRewardJsonConverter.Write(writer, itemReward);
                break;
            case MasteryPointReward masteryPointReward:
                MasteryPointRewardJsonConverter.Write(writer, masteryPointReward);
                break;
            case TitleReward titleReward:
                TitleRewardJsonConverter.Write(writer, titleReward);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                writer.WriteEndObject();
                break;
        }
    }
}
