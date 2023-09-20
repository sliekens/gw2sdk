namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record Bonus
{
    public required BonusKind Kind { get; init; }

    public required TeamColor Owner { get; init; }
}
