using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record Map
{
    public required int Id { get; init; }

    public required MapKind Kind { get; init; }

    public required Distribution Scores { get; init; }

    public required IReadOnlyCollection<Bonus> Bonuses { get; init; }

    public required IReadOnlyCollection<Objective> Objectives { get; init; }

    public required Distribution Deaths { get; init; }

    public required Distribution Kills { get; init; }
}
