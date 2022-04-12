using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Tokens;

[PublicAPI]
[DataTransferObject]
public sealed record CreatedSubtoken
{
    public string Subtoken { get; init; } = "";
}