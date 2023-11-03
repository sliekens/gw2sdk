namespace GuildWars2.Builds.Skills.Facts;

/// <summary>Describes a number of something as referenced by the <see cref="SkillFact.Text" />.</summary>
[PublicAPI]
public sealed record Number : SkillFact
{
    /// <summary>The number of something referenced by the <see cref="SkillFact.Text" />. For example, if the text is "Conditions
    /// Removed" then <see cref="Value" /> is the number of conditions removed.</summary>
    public required int Value { get; init; }
}
