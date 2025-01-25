using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a guild consumable, for example a banner.</summary>
[PublicAPI]
[JsonConverter(typeof(GuildConsumableRecipeJsonConverter))]
public sealed record GuildConsumableRecipe : Recipe
{
    /// <summary>The ingredients from guild storage required to craft the consumable.</summary>
    public required IReadOnlyList<GuildIngredient> GuildIngredients { get; init; }

    /// <summary>The guild upgrade ID of the crafted consumable.</summary>
    public required int OutputUpgradeId { get; init; }

    /// <inheritdoc />
    public bool Equals(GuildConsumableRecipe? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other)
            && GuildIngredients.SequenceEqual(other.GuildIngredients)
            && OutputUpgradeId == other.OutputUpgradeId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());
        foreach (var ingredient in GuildIngredients)
        {
            hash.Add(ingredient);
        }

        hash.Add(OutputUpgradeId);

        return hash.ToHashCode();
    }
}
