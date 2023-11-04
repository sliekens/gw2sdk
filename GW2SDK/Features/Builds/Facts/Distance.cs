namespace GuildWars2.Builds.Facts;

/// <summary>The distance of the skill/trait's effect, such as leap distance.</summary>
[PublicAPI]
public sealed record Distance : Fact
{
    /// <summary>The distance expressed in inches.</summary>
    public required int Length { get; init; }
}
