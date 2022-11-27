using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Chapter
{
    public required string Name { get; init; }
}
