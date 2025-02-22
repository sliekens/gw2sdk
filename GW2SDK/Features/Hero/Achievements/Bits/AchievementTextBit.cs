using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

/// <summary>Used by regular (not collection) achievements to describe something that needs to be done.</summary>
[PublicAPI]
[JsonConverter(typeof(AchievementTextBitJsonConverter))]
public sealed record AchievementTextBit : AchievementBit
{
    /// <summary>Describes what the player needs to do to complete this bit. For example if it is an explorer achievement, this
    /// would contain the name of an area to discover.</summary>
    public required string Text { get; init; }
}
