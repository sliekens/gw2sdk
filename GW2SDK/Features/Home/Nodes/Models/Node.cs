using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Home.Nodes.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Node
{
    public string Id { get; init; } = "";
}
