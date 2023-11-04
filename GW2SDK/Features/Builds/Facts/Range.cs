namespace GuildWars2.Builds.Facts;

/// <summary>The range of a ranged skill/trait.</summary>
[PublicAPI]
public sealed record Range : Fact
{
    /// <summary>The maximum distance, expressed in inches.</summary>
    public required int Distance { get; init; }
}
