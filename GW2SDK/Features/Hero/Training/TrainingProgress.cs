namespace GuildWars2.Hero.Training;

[PublicAPI]
[DataTransferObject]
public sealed record TrainingProgress
{
    /// <summary>The ID of the current training track. Related data can be resolved from <see cref="Profession.Training" />.</summary>
    public required int Id { get; init; }

    /// <summary>The number of hero points spent in this track.</summary>
    public required int Spent { get; init; }

    /// <summary>Whether the current training is completed.</summary>
    public required bool Done { get; init; }
}
