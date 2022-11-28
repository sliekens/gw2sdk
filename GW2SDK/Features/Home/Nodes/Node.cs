using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Home.Nodes;

[PublicAPI]
[DataTransferObject]
public sealed record Node
{
    public required string Id { get; init; }
}
