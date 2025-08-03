using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Titles;

internal sealed class TitleJsonConverter : JsonConverter<Title>
{
    public override Title Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Title value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Title Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Achievements =
                json.GetProperty("achievements")
                    .GetNullableList(static (in JsonElement achievement) => achievement.GetInt32()),
            AchievementPointsRequired =
                json.GetProperty("achievement_points_required").GetNullableInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, Title value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WritePropertyName("achievements");
        if (value.Achievements is not null)
        {
            writer.WriteStartArray();
            foreach (int achievement in value.Achievements)
            {
                writer.WriteNumberValue(achievement);
            }

            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WritePropertyName("achievement_points_required");
        if (value.AchievementPointsRequired.HasValue)
        {
            writer.WriteNumberValue(value.AchievementPointsRequired.Value);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteEndObject();
    }
}
