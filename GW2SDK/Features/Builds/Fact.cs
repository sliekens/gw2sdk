namespace GuildWars2.Builds;

/// <summary>An effect applied by the skill/trait.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Fact
{
    /// <summary>A brief summary of the effect</summary>
    public required string Text { get; init; }

    /// <summary>The URL of the fact's icon that appears in the tooltip of the skill/trait.</summary>
    public required string IconHref { get; init; }
}
