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
