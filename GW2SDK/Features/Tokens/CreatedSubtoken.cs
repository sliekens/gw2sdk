using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Tokens;

[PublicAPI]
[DataTransferObject]
public sealed record CreatedSubtoken
{
    public required string Subtoken { get; init; }
}
