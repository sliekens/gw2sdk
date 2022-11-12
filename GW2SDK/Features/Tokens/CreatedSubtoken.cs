using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Tokens;

[PublicAPI]
[DataTransferObject]
public sealed record CreatedSubtoken
{
    public required string Subtoken { get; init; }
}
