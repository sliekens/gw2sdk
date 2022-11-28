using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Files;

[PublicAPI]
[DataTransferObject]
public sealed record File
{
    public required string Id { get; init; }

    public required string Icon { get; init; }
}
