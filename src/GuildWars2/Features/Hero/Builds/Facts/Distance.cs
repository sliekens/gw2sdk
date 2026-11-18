using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The distance of the skill/trait's effect, such as leap distance.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Distance : Fact
{
    /// <summary>The distance expressed in inches.</summary>
    public required int Length { get; init; }
}
