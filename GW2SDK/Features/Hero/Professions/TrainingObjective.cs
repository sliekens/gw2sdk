namespace GuildWars2.Hero.Professions;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TrainingObjective
{
    public required int Cost { get; init; }
}
