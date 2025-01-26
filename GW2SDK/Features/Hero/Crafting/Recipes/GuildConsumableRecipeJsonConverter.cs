using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal sealed class GuildConsumableRecipeJsonConverter : JsonConverter<GuildConsumableRecipe>
{
    public const string DiscriminatorValue = "guild_consumable_recipe";

    public override GuildConsumableRecipe Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildConsumableRecipe value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildConsumableRecipe Read(JsonElement json)
    {
        if (!json.GetProperty(RecipeJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(RecipeJsonConverter.DiscriminatorName).GetString());
        }

        return new GuildConsumableRecipe
        {
            Id = json.GetProperty("id").GetInt32(),
            OutputItemId = json.GetProperty("output_item_id").GetInt32(),
            OutputItemCount = json.GetProperty("output_item_count").GetInt32(),
            MinRating = json.GetProperty("min_rating").GetInt32(),
            TimeToCraft = TimeSpan.FromMilliseconds(json.GetProperty("time_to_craft_ms").GetDouble()),
            Disciplines = json.GetProperty("disciplines").GetList(static value => value.GetEnum<CraftingDisciplineName>()),
            Flags = RecipeFlagsJsonConverter.Read(json.GetProperty("flags")),
            Ingredients = json.GetProperty("ingredients").GetList(IngredientJsonConverter.Read),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            GuildIngredients = json.GetProperty("guild_ingredients").GetList(GuildIngredientJsonConverter.Read),
            OutputUpgradeId = json.GetProperty("output_upgrade_id").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildConsumableRecipe value)
    {
        writer.WriteStartObject();
        writer.WriteString(RecipeJsonConverter.DiscriminatorName, DiscriminatorValue);
        RecipeJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteStartArray("guild_ingredients");
        foreach (var guildIngredient in value.GuildIngredients)
        {
            GuildIngredientJsonConverter.Write(writer, guildIngredient);
        }
        writer.WriteEndArray();
        writer.WriteNumber("output_upgrade_id", value.OutputUpgradeId);
        writer.WriteEndObject();
    }
}

