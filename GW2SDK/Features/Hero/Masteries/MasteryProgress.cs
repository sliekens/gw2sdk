namespace GuildWars2.Hero.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryProgress
{
    public required int Id { get; init; }

    public required int Level { get; init; }
}
