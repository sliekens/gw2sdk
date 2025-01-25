﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal sealed class ConsumableRecipeJsonConverter : JsonConverter<ConsumableRecipe>
{
    public const string DiscriminatorValue = "consumable_recipe";

    public override ConsumableRecipe Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, ConsumableRecipe value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static ConsumableRecipe Read(JsonElement json)
    {
        if (!json.GetProperty(RecipeJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(RecipeJsonConverter.DiscriminatorName).GetString());
        }

        return new ConsumableRecipe
        {
            Id = json.GetProperty("id").GetInt32(),
            OutputItemId = json.GetProperty("output_item_id").GetInt32(),
            OutputItemCount = json.GetProperty("output_item_count").GetInt32(),
            MinRating = json.GetProperty("min_rating").GetInt32(),
            TimeToCraft = TimeSpan.FromMilliseconds(json.GetProperty("time_to_craft_ms").GetDouble()),
            Disciplines = json.GetProperty("disciplines").GetList(static value => value.GetEnum<CraftingDisciplineName>()),
            Flags = RecipeFlagsJsonConverter.Read(json.GetProperty("flags")),
            Ingredients = json.GetProperty("ingredients").GetList(IngredientJsonConverter.Read),
            ChatLink = json.GetProperty("chat_link").GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, ConsumableRecipe value)
    {
        writer.WriteStartObject();
        writer.WriteString(RecipeJsonConverter.DiscriminatorName, DiscriminatorValue);
        RecipeJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
