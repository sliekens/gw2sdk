using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Bits;

/// <summary>Used by regular (not collection) achievements to describe something that needs to be done.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record AchievementTextBit : AchievementBit;
