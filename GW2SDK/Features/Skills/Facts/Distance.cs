namespace GuildWars2.Skills.Facts;

/// <summary>The distance of the skill's effect, such as leap distance.</summary>
[PublicAPI]
public sealed record Distance : SkillFact
{
    /// <summary>The distance expressed in inches.</summary>
    public required int Length { get; init; }
}
