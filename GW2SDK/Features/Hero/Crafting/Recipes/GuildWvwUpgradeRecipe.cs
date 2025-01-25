using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a recipe for crafting a guild WvW upgrade: schematics, blueprints, siege weapons.</summary>
[PublicAPI]
[JsonConverter(typeof(GuildWvwUpgradeRecipeJsonConverter))]
public sealed record GuildWvwUpgradeRecipe : Recipe
{
    /// <summary>The guild upgrade ID of the crafted upgrade, or <c>null</c> if the recipe is for a component like Vial of
    /// Enchanted Water.</summary>
    public required int? OutputUpgradeId { get; init; }

    /// <inheritdoc />
    public bool Equals(GuildWvwUpgradeRecipe? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && OutputUpgradeId == other.OutputUpgradeId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), OutputUpgradeId);
    }
}
