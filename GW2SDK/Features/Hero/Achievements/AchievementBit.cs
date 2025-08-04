using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

/// <summary>The base type for achievement bits. Cast objects of this type to a more specific type to access more
/// properties.</summary>
[Inheritable]
[DataTransferObject]
[JsonConverter(typeof(AchievementBitJsonConverter))]
public record AchievementBit
{
    /// <summary>Describes what the player needs to do to complete this bit. For example if it is an explorer achievement, this
    /// would contain the name of an area to discover.</summary>
    public required string Text { get; init; }
}
