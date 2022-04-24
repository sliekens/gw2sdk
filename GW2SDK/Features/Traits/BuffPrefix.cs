using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record BuffPrefix
{
    public string Text { get; init; } = "";

    public string Icon { get; init; } = "";

    public string Status { get; init; } = "";

    public string Description { get; init; } = "";
}
