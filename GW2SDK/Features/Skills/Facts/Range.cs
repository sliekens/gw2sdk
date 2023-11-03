namespace GuildWars2.Skills.Facts;

/// <summary>The range of a ranged skill.</summary>
[PublicAPI]
public sealed record Range : SkillFact
{
    /// <summary>The maximum distance, expressed in inches.</summary>
    public required int Distance { get; init; }
}
