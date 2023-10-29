﻿namespace GuildWars2.Armory;

/// <summary>Information about how many legendary items are stored in the legendary armory of the current account.</summary>
[PublicAPI]
public sealed record BoundLegendaryItem
{
    /// <summary>The item id.</summary>
    public required int Id { get; init; }

    /// <summary>How many of the current legendary item are bound to the account.</summary>
    public required int Count { get; init; }
}
