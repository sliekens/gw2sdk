namespace GuildWars2.Hero.Training;

[PublicAPI]
public sealed record TraitObjective : TrainingObjective
{
    public required int TraitId { get; init; }
}
