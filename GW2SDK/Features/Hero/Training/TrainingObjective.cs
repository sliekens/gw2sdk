namespace GuildWars2.Hero.Training;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TrainingObjective
{
    public required int Cost { get; init; }
}
