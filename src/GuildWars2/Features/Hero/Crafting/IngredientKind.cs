using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting;

/// <summary>The ingredient kinds used in crafting recipes.</summary>
[DefaultValue(Item)]
[JsonConverter(typeof(IngredientKindJsonConverter))]
public enum IngredientKind
{
    /// <summary>The ingredient is an item, for example planks.</summary>
    Item,

    /// <summary>The ingredient is a currency, for example research notes.</summary>
    Currency,

    /// <summary>The ingredient is a guild upgrade, for example a decoration which is used to craft a larger decoration.</summary>
    GuildUpgrade
}
