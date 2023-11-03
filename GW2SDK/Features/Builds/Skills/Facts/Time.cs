namespace GuildWars2.Builds.Skills.Facts;

/// <summary>Describes the duration of an effect as referenced by the <see cref="SkillFact.Text" />.</summary>
[PublicAPI]
public sealed record Time : SkillFact
{
    /// <summary>The duration of something referenced by <see cref="SkillFact.Text" />.</summary>
    public required TimeSpan Duration { get; init; }
}
