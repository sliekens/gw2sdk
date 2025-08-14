using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Disciplines;

internal sealed class
    LearnedCraftingDisciplinesJsonConverter : JsonConverter<LearnedCraftingDisciplines>
{
    public override LearnedCraftingDisciplines? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public static LearnedCraftingDisciplines Read(in JsonElement json)
    {
        return new()
        {
            Disciplines = json.GetProperty("disciplines")
                .GetList(CraftingDisciplineJsonConverter.Read)!
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        LearnedCraftingDisciplines value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static void Write(Utf8JsonWriter writer, LearnedCraftingDisciplines value)
    {
        writer.WriteStartObject();
        writer.WriteStartArray("disciplines");
        foreach (CraftingDiscipline discipline in value.Disciplines)
        {
            CraftingDisciplineJsonConverter.Write(writer, discipline);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
