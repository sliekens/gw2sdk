using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal sealed class AmuletRecipeJsonConverter : JsonConverter<AmuletRecipe>
{
    public const string DiscriminatorValue = "amulet_recipe";

    public override AmuletRecipe Read(
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
        AmuletRecipe value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static AmuletRecipe Read(in JsonElement json)
    {
        if (!json.GetProperty(RecipeJsonConverter.DiscriminatorName)
            .ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(
                json.GetProperty(RecipeJsonConverter.DiscriminatorName).GetString()
            );
        }

        return new AmuletRecipe
        {
            Id = json.GetProperty("id").GetInt32(),
            OutputItemId = json.GetProperty("output_item_id").GetInt32(),
            OutputItemCount = json.GetProperty("output_item_count").GetInt32(),
            MinRating = json.GetProperty("min_rating").GetInt32(),
            TimeToCraft =
                TimeSpan.FromMilliseconds(json.GetProperty("time_to_craft_ms").GetDouble()),
            Disciplines =
                json.GetProperty("disciplines")
                    .GetList(static (in JsonElement value) => value.GetEnum<CraftingDisciplineName>()),
            Flags = RecipeFlagsJsonConverter.Read(json.GetProperty("flags")),
            Ingredients = json.GetProperty("ingredients").GetList(IngredientJsonConverter.Read),
            ChatLink = json.GetProperty("chat_link").GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, AmuletRecipe value)
    {
        writer.WriteStartObject();
        writer.WriteString(RecipeJsonConverter.DiscriminatorName, DiscriminatorValue);
        RecipeJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
