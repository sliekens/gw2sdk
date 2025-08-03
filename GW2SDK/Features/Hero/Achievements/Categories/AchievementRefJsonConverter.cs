using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal sealed class AchievementRefJsonConverter : JsonConverter<AchievementRef>
{
    public override AchievementRef Read(
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
        AchievementRef value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementRef Read(in JsonElement json)
    {
        return new()
        {
            Id = json.GetProperty("id").GetInt32(),
            Flags = AchievementFlagsJsonConverter.Read(json.GetProperty("flags")),
            Level = json.GetProperty("level").GetNullable(LevelRequirementJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementRef value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WritePropertyName("flags");
        AchievementFlagsJsonConverter.Write(writer, value.Flags);
        writer.WritePropertyName("level");
        if (value.Level is not null)
        {
            LevelRequirementJsonConverter.Write(writer, value.Level);
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteEndObject();
    }
}
