using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
public sealed record BoundLegendaryItem
{
    /// <summary>The item id.</summary>
    public int Id { get; init; }

    /// <summary>The number of items that are bound to the current account.</summary>
    public int Count { get; init; }
}
