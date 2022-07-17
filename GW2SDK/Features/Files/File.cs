using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Files;

[PublicAPI]
[DataTransferObject]
public sealed record File
{
    public string Id { get; init; } = "";

    public string Icon { get; init; } = "";
}
