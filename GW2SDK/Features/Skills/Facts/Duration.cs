namespace GuildWars2.Skills.Facts;

/// <summary>The duration of the skill.</summary>
[PublicAPI]
public sealed record Duration : SkillFact
{
    /// <summary>The duration.</summary>
    public required TimeSpan Length { get; init; }
}
