using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

/// <summary>Used in collection achievements to describe a skin that needs to be obtained.</summary>
[JsonConverter(typeof(AchievementSkinBitJsonConverter))]
public sealed record AchievementSkinBit : AchievementBit
{
    /// <summary>The ID of the skin that needs to be bound to the account.</summary>
    public required int Id { get; init; }
}
