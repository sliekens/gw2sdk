using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    public required int Id { get; init; }
}
