namespace GuildWars2.Hero.Achievements;

/// <summary>Used in collection achievements to describe a skin that needs to be obtained.</summary>
[PublicAPI]
public sealed record AchievementSkinBit : AchievementBit
{
    /// <summary>The ID of the skin that needs to be bound to the account.</summary>
    public required int Id { get; init; }
}
