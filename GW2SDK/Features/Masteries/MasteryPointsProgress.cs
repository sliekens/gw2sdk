namespace GuildWars2.Masteries;

[PublicAPI]
public sealed record MasteryPointsProgress
{
    public required IReadOnlyCollection<MasteryPointsTotal> Totals { get; init; }

    public required IReadOnlyCollection<int> Unlocked { get; init; }
}
