using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Items;

namespace MostVersatileMaterials;

public class ReferenceData
{
    public required List<Recipe> Recipes { get; init; }

    public required Dictionary<int, Item> InputItems { get; init; }

    public required Dictionary<int, Item> OutputItems { get; init; }
}
