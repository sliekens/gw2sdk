namespace GuildWars2.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record CharacterTraining
{
    public required IReadOnlyCollection<TrainingProgress> Training { get; init; }
}
