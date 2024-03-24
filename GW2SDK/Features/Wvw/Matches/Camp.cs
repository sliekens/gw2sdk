namespace GuildWars2.Wvw.Matches;

/// <summary>Information about a camp.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Camp : Objective
{
    /// <summary>The ID of the guild that claimed the camp.</summary>
    public required string ClaimedBy { get; init; }

    /// <summary>The date and time when the camp was claimed.</summary>
    public required DateTimeOffset? ClaimedAt { get; init; }

    /// <summary>How many yaks have been delivered to the camp.</summary>
    public required int YaksDelivered { get; init; }

    /// <summary>The IDs of active guild upgrades.</summary>
    public required IReadOnlyCollection<int> GuildUpgrades { get; init; }
}
