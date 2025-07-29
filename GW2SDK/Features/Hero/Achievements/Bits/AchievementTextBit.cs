using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

/// <summary>Used by regular (not collection) achievements to describe something that needs to be done.</summary>
[PublicAPI]
[JsonConverter(typeof(AchievementTextBitJsonConverter))]
public sealed record AchievementTextBit : AchievementBit;
