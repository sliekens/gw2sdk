namespace GuildWars2.Wvw.Matches;

/// <summary>Information about the ownership of a tower.</summary>
[DataTransferObject]
public sealed record OwnedTower : OwnedObjective
{
    /// <summary>The ID of the guild that claimed the tower.</summary>
    public required string ClaimedBy { get; init; }

    /// <summary>The date and time when the tower was claimed.</summary>
    public required DateTimeOffset? ClaimedAt { get; init; }

    /// <summary>How many yaks have been delivered to the tower.</summary>
    public required int YaksDelivered { get; init; }

    /// <summary>The IDs of active guild upgrades.</summary>
    public required IImmutableValueList<int> GuildUpgrades { get; init; }
}
