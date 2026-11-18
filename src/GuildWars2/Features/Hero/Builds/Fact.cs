using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

/// <summary>An effect applied by the skill/trait.</summary>
[Inheritable]
[DataTransferObject]
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public record Fact
{
    /// <summary>A brief summary of the effect</summary>
    public required string Text { get; init; }

    /// <summary>The URL of the fact icon as it appears in the tooltip of the skill/trait.</summary>
    public required Uri? IconUrl { get; init; }
}
