namespace GuildWars2.Wvw.Abilities;

[PublicAPI]
[DataTransferObject]
public sealed record AbilityRank
{
    public required int Cost { get; init; }

    public required string Effect { get; init; }
}
