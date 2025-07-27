using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting;

internal sealed class IngredientJsonConverter : JsonConverter<Ingredient>
{
    public override Ingredient Read(
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
        Ingredient value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static Ingredient Read(in JsonElement json)
    {
        return new Ingredient
        {
            Kind = json.GetProperty("kind").GetEnum<IngredientKind>(),
            Id = json.GetProperty("id").GetInt32(),
            Count = json.GetProperty("count").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, Ingredient value)
    {
        writer.WriteStartObject();
        writer.WriteString("kind", value.Kind.ToString());
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("count", value.Count);
        writer.WriteEndObject();
    }
}
