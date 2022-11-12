using System.ComponentModel;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
[DataTransferObject]
public sealed record Ingredient
{
    public required IngredientKind Kind { get; init; }

    public required int Id { get; init; }

    public required int Count { get; init; }
}

[PublicAPI]
[DefaultValue(Item)]
public enum IngredientKind
{
    Item,

    Currency,

    GuildUpgrade
}
