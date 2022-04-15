using System.ComponentModel;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Crafting.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Ingredient
{
    public IngredientKind Kind { get; init; }

    public int Id { get; init; }

    public int Count { get; init; }
}

[PublicAPI]
[DefaultValue(Item)]
public enum IngredientKind
{
    Item,

    Currency,

    GuildUpgrade
}
