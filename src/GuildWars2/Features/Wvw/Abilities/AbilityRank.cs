namespace GuildWars2.Wvw.Abilities;

/// <summary>A rank of an ability that can be trained in World vs. World by earning World Experience (WXP) and spending
/// World Ability points.</summary>
[DataTransferObject]
public sealed record AbilityRank
{
    /// <summary>The cost in World Ability points to train this rank.</summary>
    public required int Cost { get; init; }

    /// <summary>Describes the effect that is unlocked by training this rank.</summary>
    public required string Effect { get; init; }
}
