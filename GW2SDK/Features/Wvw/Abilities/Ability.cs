namespace GuildWars2.Wvw.Abilities;

[PublicAPI]
[DataTransferObject]
public sealed record Ability
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    /// <summary>The URL of the ability icon.</summary>
    public required string IconHref { get; init; }

    public required IReadOnlyCollection<AbilityRank> Ranks { get; init; }
}
