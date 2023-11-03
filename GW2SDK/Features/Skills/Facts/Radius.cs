namespace GuildWars2.Skills.Facts;

/// <summary>The radius of the skill effect, such as blast radius.</summary>
[PublicAPI]
public sealed record Radius : SkillFact
{
    /// <summary>The distance of the radius, expressed in inches.</summary>
    public required int Distance { get; init; }
}
