namespace GuildWars2.Hero.Masteries;

/// <summary>Information about mastery points on the account.</summary>
public sealed record MasteryPointsProgress
{
    /// <summary>The aggregated totals of mastery points on the account by region.</summary>
    public required IImmutableValueList<MasteryPointsTotal> Totals { get; init; }

    /// <summary>The IDs of mastery points that have been unlocked on the account. This is not the same as the mastery IDs.</summary>
    public required IImmutableValueList<int> Unlocked { get; init; }
}
