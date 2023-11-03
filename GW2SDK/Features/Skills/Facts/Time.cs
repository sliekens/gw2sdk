namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record Time : SkillFact
{
    public required TimeSpan Duration { get; init; }
}
