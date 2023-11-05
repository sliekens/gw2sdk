namespace GuildWars2.Hero.Equipment;

/// <summary>Information about a legendary item and how many of them can be stored in the legendary armory.</summary>
[PublicAPI]
public sealed record LegendaryItem
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>How many of them can be added to the legendary armory.</summary>
    public required int MaxCount { get; init; }
}
