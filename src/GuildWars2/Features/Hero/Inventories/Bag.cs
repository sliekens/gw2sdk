namespace GuildWars2.Hero.Inventories;

/// <summary>Information about an equipped bag.</summary>
[DataTransferObject]
public sealed record Bag
{
    /// <summary>The current bag's item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The current bag's capacity.</summary>
    public required int Size { get; init; }

    /// <summary>The current bag's inventory.</summary>
    public required Inventory Inventory { get; init; }
}
