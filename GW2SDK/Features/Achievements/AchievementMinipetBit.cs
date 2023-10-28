namespace GuildWars2.Achievements;

/// <summary>Used in collection achievements to describe a minipet that needs to be obtained.</summary>
[PublicAPI]
public sealed record AchievementMinipetBit : AchievementBit
{
    /// <summary>The ID of the minipet that needs to be bound to the account.</summary>
    public required int Id { get; init; }
}
