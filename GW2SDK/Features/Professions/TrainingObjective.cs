namespace GuildWars2.Professions;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TrainingObjective
{
    public required int Cost { get; init; }
}
