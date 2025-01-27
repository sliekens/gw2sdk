using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Disciplines;

internal sealed class CraftingDisciplineJsonConverter : JsonConverter<CraftingDiscipline>
{
    public override CraftingDiscipline? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static CraftingDiscipline Read(JsonElement json)
    {
        return new CraftingDiscipline
        {
            Discipline = json.GetProperty("discipline").GetEnum<CraftingDisciplineName>(),
            Rating = json.GetProperty("rating").GetInt32(),
            Active = json.GetProperty("active").GetBoolean()
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        CraftingDiscipline value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, CraftingDiscipline value)
    {
        writer.WriteStartObject();
        writer.WriteString("discipline", value.Discipline.ToString());
        writer.WriteNumber("rating", value.Rating);
        writer.WriteBoolean("active", value.Active);
        writer.WriteEndObject();
    }
}
