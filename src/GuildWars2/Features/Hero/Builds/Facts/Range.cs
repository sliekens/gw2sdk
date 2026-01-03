namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The range of a ranged skill/trait.</summary>
public sealed record Range : Fact
{
    /// <summary>The maximum distance, expressed in inches.</summary>
    public required int Distance { get; init; }
}
