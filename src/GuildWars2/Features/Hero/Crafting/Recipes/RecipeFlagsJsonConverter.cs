using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal sealed class RecipeFlagsJsonConverter : JsonConverter<RecipeFlags>
{
    public override RecipeFlags? Read(
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
        RecipeFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static RecipeFlags Read(in JsonElement json)
    {
        return new()
        {
            AutoLearned = json.GetProperty("auto_learned").GetBoolean(),
            LearnedFromItem = json.GetProperty("learned_from_item").GetBoolean(),
            Other = json.GetProperty("other").GetList(static (in value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, RecipeFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("auto_learned", value.AutoLearned);
        writer.WriteBoolean("learned_from_item", value.LearnedFromItem);
        writer.WriteStartArray("other");
        foreach (string other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
