using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record BuffPrefix
{
    public required string Text { get; init; }

    public required string Icon { get; init; }

    public required string Status { get; init; }

    public required string Description { get; init; }
}
