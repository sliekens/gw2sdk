namespace GuildWars2.Hero.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryLevel
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Instruction { get; init; }

    public required string Icon { get; init; }

    public required int PointCost { get; init; }

    public required int ExperienceCost { get; init; }
}
