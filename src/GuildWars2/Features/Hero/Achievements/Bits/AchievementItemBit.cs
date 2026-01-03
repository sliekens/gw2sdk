using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

/// <summary>Used in collection achievements to describe an item that needs to be obtained.</summary>
[JsonConverter(typeof(AchievementItemBitJsonConverter))]
public sealed record AchievementItemBit : AchievementBit
{
    /// <summary>The ID of the item that needs to be bound or consumed by the account.</summary>
    public required int Id { get; init; }
}
