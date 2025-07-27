using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal sealed class AchievementCategoryJsonConverter : JsonConverter<AchievementCategory>
{
    public override AchievementCategory Read(
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
        AchievementCategory value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AchievementCategory Read(in JsonElement json)
    {
        var iconString = json.GetProperty("icon").GetStringRequired();
        return new AchievementCategory
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Order = json.GetProperty("order").GetInt32(),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Achievements = json.GetProperty("achievements")
                .GetList(AchievementRefJsonConverter.Read),
            Tomorrow = json.GetProperty("tomorrow")
                .GetNullableList(AchievementRefJsonConverter.Read)
        };
    }

    public static void Write(Utf8JsonWriter writer, AchievementCategory value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteString("name", value.Name);
        writer.WriteString("description", value.Description);
        writer.WriteNumber("order", value.Order);
        writer.WriteString("icon", value.IconUrl?.ToString());
        writer.WritePropertyName("achievements");
        writer.WriteStartArray();
        foreach (var achievement in value.Achievements)
        {
            AchievementRefJsonConverter.Write(writer, achievement);
        }

        writer.WriteEndArray();
        writer.WritePropertyName("tomorrow");
        if (value.Tomorrow != null)
        {
            writer.WriteStartArray();
            foreach (var achievement in value.Tomorrow)
            {
                AchievementRefJsonConverter.Write(writer, achievement);
            }

            writer.WriteEndArray();
        }
        else
        {
            writer.WriteNullValue();
        }

        writer.WriteEndObject();
    }
}
