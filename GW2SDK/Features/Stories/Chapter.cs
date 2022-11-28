using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Stories;

[PublicAPI]
[DataTransferObject]
public sealed record Chapter
{
    public required string Name { get; init; }
}
