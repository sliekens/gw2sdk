namespace GuildWars2.Hero.Training;

/// <summary>Information about the number of hero points spent in a training track.</summary>
[DataTransferObject]
public sealed record TrainingProgress
{
    /// <summary>The ID of the training track.</summary>
    public required int Id { get; init; }

    /// <summary>The number of hero points spent in this track.</summary>
    public required int Spent { get; init; }

    /// <summary>Whether the current training is completed.</summary>
    public required bool Done { get; init; }
}
