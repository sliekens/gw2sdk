using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.MapChests;

[PublicAPI]
[DataTransferObject]
public sealed record MapChest
{
    public required string Id { get; init; }
}
