using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Files;

[PublicAPI]
[DataTransferObject]
public sealed record File
{
    public required string Id { get; init; }

    public required string Icon { get; init; }
}
