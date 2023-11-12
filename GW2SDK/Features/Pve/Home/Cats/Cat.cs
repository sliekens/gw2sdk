namespace GuildWars2.Pve.Home.Cats;

[PublicAPI]
[DataTransferObject]
public sealed record Cat
{
    public required int Id { get; init; }

    public required string Hint { get; init; }
}
