using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record Bag
{
    /// <summary>The current bag's item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The current bag's capacity.</summary>
    public required int Size { get; init; }

    /// <summary>The current bag's inventory, sorted by in-game order. Enumerated values can contain <c>null</c> when some item
    /// slots are empty.</summary>
    public required Inventory Inventory { get; init; }
}
