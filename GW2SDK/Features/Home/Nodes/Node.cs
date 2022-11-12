using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Home.Nodes;

[PublicAPI]
[DataTransferObject]
public sealed record Node
{
    public required string Id { get; init; }
}
