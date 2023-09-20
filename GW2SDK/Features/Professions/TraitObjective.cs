namespace GuildWars2.Professions;

[PublicAPI]
public sealed record TraitObjective : TrainingObjective
{
    public required int TraitId { get; init; }
}
