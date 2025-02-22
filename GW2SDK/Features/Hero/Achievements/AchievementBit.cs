using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

/// <summary>The base type for achievement bits. Cast objects of this type to a more specific type to access more
/// properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
[JsonConverter(typeof(AchievementBitJsonConverter))]
public record AchievementBit;
