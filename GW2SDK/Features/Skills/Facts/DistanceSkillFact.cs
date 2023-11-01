namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record DistanceSkillFact : SkillFact
{
    public required int Distance { get; init; }
}
