namespace GuildWars2.Hero.Training;

/// <summary>Information about the progress of a character's training.</summary>
[DataTransferObject]
public sealed record CharacterTraining
{
    /// <summary>The character's training tracks.</summary>
    public required IReadOnlyList<TrainingProgress> Training { get; init; }
}
