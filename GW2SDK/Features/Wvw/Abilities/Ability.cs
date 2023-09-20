namespace GuildWars2.Wvw.Abilities;

[PublicAPI]
[DataTransferObject]
public sealed record Ability
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Icon { get; init; }

    public required IReadOnlyCollection<AbilityRank> Ranks { get; init; }
}
