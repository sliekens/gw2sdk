namespace GuildWars2.Hero.Training;

[PublicAPI]
[DataTransferObject]
public sealed record CharacterTraining
{
    public required IReadOnlyCollection<TrainingProgress> Training { get; init; }
}
