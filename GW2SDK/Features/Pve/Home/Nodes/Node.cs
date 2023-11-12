namespace GuildWars2.Pve.Home.Nodes;

[PublicAPI]
[DataTransferObject]
public sealed record Node
{
    public required string Id { get; init; }
}
