namespace GuildWars2.Hero.Training.Objectives;

/// <summary>Informtion about a trait that can be unlocked by spending hero points.</summary>
public sealed record TraitObjective : TrainingObjective
{
    /// <summary>The trait ID.</summary>
    public required int TraitId { get; init; }
}
