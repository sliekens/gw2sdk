namespace GuildWars2.Pve.MapChests;

[PublicAPI]
[DataTransferObject]
public sealed record MapChest
{
    public required string Id { get; init; }
}
