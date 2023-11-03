namespace GuildWars2.Builds.Skills.Facts;

/// <summary>A percentage of something as referenced by the <see cref="SkillFact.Text" />.</summary>
[PublicAPI]
public sealed record Percentage : SkillFact
{
    /// <summary>The percentage something referenced by the <see cref="SkillFact.Text" />. For example, if the text is "Health
    /// Threshold" then <see cref="Percent" /> is the health threshold for activating the skill behavior.</summary>
    public required double Percent { get; init; }
}
