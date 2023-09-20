namespace GuildWars2.Skills;

[PublicAPI]
public sealed record ComboFinisherSkillFact : SkillFact
{
    public required int Percent { get; init; }

    public required ComboFinisherName FinisherName { get; init; }
}
