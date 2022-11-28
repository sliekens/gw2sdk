using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    public required int Id { get; init; }
}
