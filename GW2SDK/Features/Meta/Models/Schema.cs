using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Meta.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Schema
{
    public string Version { get; init; } = "latest";

    public string Description { get; init; } = "";
}
