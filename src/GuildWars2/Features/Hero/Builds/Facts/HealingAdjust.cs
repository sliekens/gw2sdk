using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The skill pulses heals when a condition is met.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record HealingAdjust : Fact
{
    /// <summary>How many times the skill pulses heals when a condition is met.</summary>
    public required int HitCount { get; init; }
}
