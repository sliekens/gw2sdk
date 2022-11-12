using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record Schema
{
    public required string Version { get; init; }

    public required string Description { get; init; }
}
