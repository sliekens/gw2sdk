using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.V2;

[PublicAPI]
[DataTransferObject]
public sealed record ApiVersion
{
    public string Version { get; init; } = "latest";

    public string Description { get; init; } = "";
}