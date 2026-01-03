using System.Collections.ObjectModel;

using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Items;

namespace MostVersatileMaterials;

internal sealed class ReferenceData
{
    public required ReadOnlyCollection<Recipe> Recipes { get; init; }

    public required Dictionary<int, Item> InputItems { get; init; }

    public required Dictionary<int, Item> OutputItems { get; init; }
}
