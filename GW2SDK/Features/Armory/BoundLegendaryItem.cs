namespace GuildWars2.Armory;

[PublicAPI]
public sealed record BoundLegendaryItem
{
    /// <summary>The item id.</summary>
    public required int Id { get; init; }

    /// <summary>The number of items that are bound to the current account.</summary>
    public required int Count { get; init; }
}
