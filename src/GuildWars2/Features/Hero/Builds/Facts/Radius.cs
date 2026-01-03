namespace GuildWars2.Hero.Builds.Facts;

/// <summary>The radius of the skill/trait's effect, such as blast radius.</summary>
public sealed record Radius : Fact
{
    /// <summary>The distance of the radius, expressed in inches.</summary>
    public required int Distance { get; init; }
}
