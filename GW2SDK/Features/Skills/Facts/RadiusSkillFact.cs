namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record RadiusSkillFact : SkillFact
{
    public required int Distance { get; init; }
}
