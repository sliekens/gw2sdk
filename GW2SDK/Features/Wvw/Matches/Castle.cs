namespace GuildWars2.Wvw.Matches;

/// <summary>
/// Information about a castle.
/// </summary>
[PublicAPI]
[DataTransferObject]
public sealed record Castle : Objective
{
    /// <summary>The ID of the guild that claimed the castle.</summary>
    public required string ClaimedBy { get; init; }

    /// <summary>The date and time when the castle was claimed.</summary>
    public required DateTimeOffset? ClaimedAt { get; init; }

    /// <summary>How many yaks have been delivered to the castle.</summary>
    public required int YaksDelivered { get; init; }

    /// <summary>The IDs of active guild upgrades.</summary>
    public required IReadOnlyCollection<int> GuildUpgrades { get; init; }
}
