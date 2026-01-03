namespace GuildWars2.Hero.Training;

/// <summary>Information about a training track, which consists of a list of training objectives which can be completed by
/// spending hero points.</summary>
[DataTransferObject]
public sealed record Training
{
    /// <summary>The training track ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the training track, for example Signet Training.</summary>
    public required string Name { get; init; }

    /// <summary>Indicates whether the training track unlocks skills, core specializations (traits) or elite specializations
    /// (both skills and traits).</summary>
    public required Extensible<TrainingCategory> Category { get; init; }

    /// <summary>The objectives within the training track. The list type is abstract, the derived types are documented in the
    /// Objectives namespace.</summary>
    public required IReadOnlyList<TrainingObjective> Track { get; init; }
}
