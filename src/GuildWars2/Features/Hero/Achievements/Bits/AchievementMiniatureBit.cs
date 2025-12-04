using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

/// <summary>Used in collection achievements to describe a miniature that needs to be obtained.</summary>
[JsonConverter(typeof(AchievementMiniatureBitJsonConverter))]
public sealed record AchievementMiniatureBit : AchievementBit
{
    /// <summary>The ID of the miniature that needs to be bound to the account.</summary>
    public required int Id { get; init; }
}
