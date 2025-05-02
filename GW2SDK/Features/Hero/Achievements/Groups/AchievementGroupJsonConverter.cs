using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Groups;

internal sealed class AchievementGroupJsonConverter : JsonConverter<AchievementGroup>
{
    public override AchievementGroup Read(
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
        AchievementGroup value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementGroup Read(JsonElement json)
    {
        return new AchievementGroup
        {
            Id = json.GetProperty("id").GetStringRequired(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Order = json.GetProperty("order").GetInt32(),
            Categories = json.GetProperty("categories").GetList(category => category.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementGroup value)
    {
        writer.WriteStartObject();
        writer.WriteString("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WriteNumber("order", value.Order);
        writer.WritePropertyName("categories");
        writer.WriteStartArray();
        foreach (var category in value.Categories)
        {
            writer.WriteNumberValue(category);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
