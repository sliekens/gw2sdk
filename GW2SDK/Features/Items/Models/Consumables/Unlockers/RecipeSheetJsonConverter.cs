using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class RecipeSheetJsonConverter : JsonConverter<RecipeSheet>
{
    public const string DiscriminatorValue = "recipe_sheet";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(RecipeSheet).IsAssignableFrom(typeToConvert);
    }

    public override RecipeSheet Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, RecipeSheet value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static RecipeSheet Read(JsonElement json)
    {
        if (!json.GetProperty(ItemJsonConverter.DiscriminatorName).ValueEquals(ConsumableJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(ItemJsonConverter.DiscriminatorName).GetString());
        }

        if (!json.GetProperty(ConsumableJsonConverter.DiscriminatorName).ValueEquals(UnlockerJsonConverter.DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(ConsumableJsonConverter.DiscriminatorName).GetString());
        }

        if (!json.GetProperty(UnlockerJsonConverter.DiscriminatorName).ValueEquals(DiscriminatorValue))
        {
            ThrowHelper.ThrowInvalidDiscriminator(json.GetProperty(UnlockerJsonConverter.DiscriminatorName).GetString());
        }

        return new RecipeSheet
        {
            Id = json.GetProperty("id").GetInt32(),
            Name = json.GetProperty("name").GetStringRequired(),
            Description = json.GetProperty("description").GetStringRequired(),
            Level = json.GetProperty("level").GetInt32(),
            Rarity = json.GetProperty("rarity").GetEnum<Rarity>(),
            VendorValue = json.GetProperty("vendor_value").GetInt32(),
            GameTypes = json.GetProperty("game_types").GetList(static value => value.GetEnum<GameType>()),
            Flags = ItemFlagsJsonConverter.Read(json.GetProperty("flags")),
            Restrictions = ItemRestrictionJsonConverter.Read(json.GetProperty("restrictions")),
            ChatLink = json.GetProperty("chat_link").GetStringRequired(),
            IconHref = json.GetProperty("icon").GetString(),
            RecipeId = json.GetProperty("recipe_id").GetInt32(),
            ExtraRecipeIds = json.GetProperty("extra_recipe_ids").GetList(static value => value.GetInt32())
        };
    }

    public static void Write(Utf8JsonWriter writer, RecipeSheet value)
    {
        writer.WriteStartObject();
        writer.WriteString(ItemJsonConverter.DiscriminatorName, ConsumableJsonConverter.DiscriminatorValue);
        writer.WriteString(ConsumableJsonConverter.DiscriminatorName, UnlockerJsonConverter.DiscriminatorValue);
        writer.WriteString(UnlockerJsonConverter.DiscriminatorName, DiscriminatorValue);
        ItemJsonConverter.WriteCommonProperties(writer, value);
        writer.WriteNumber("recipe_id", value.RecipeId);
        writer.WritePropertyName("extra_recipe_ids");
        writer.WriteStartArray();
        foreach (var id in value.ExtraRecipeIds)
        {
            writer.WriteNumberValue(id);
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
