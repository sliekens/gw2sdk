﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal sealed class SwordRecipeJsonConverter : JsonConverter<SwordRecipe>
{
    public const string DiscriminatorValue = "sword_recipe";

    public override SwordRecipe Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, SwordRecipe value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static SwordRecipe Read(JsonElement json)
    {
        if (!json.GetProperty(RecipeJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(RecipeJsonConverter.DiscriminatorName).GetString());
        }

        return new SwordRecipe
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

    public static void Write(Utf8JsonWriter writer, SwordRecipe value)
    {
        writer.WriteStartObject();
        writer.WriteString(RecipeJsonConverter.DiscriminatorName, DiscriminatorValue);
        RecipeJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteEndObject();
    }
}
