using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Meta.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    public int Id { get; init; }
}
