namespace GuildWars2.Hero.Professions;

[PublicAPI]
public sealed record TraitObjective : TrainingObjective
{
    public required int TraitId { get; init; }
}
