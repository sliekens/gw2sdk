namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record Tower : Objective
{
    public required string ClaimedBy { get; init; }

    public required DateTimeOffset? ClaimedAt { get; init; }

    public required int YaksDelivered { get; init; }

    public required IReadOnlyCollection<int> GuildUpgrades { get; init; }
}
