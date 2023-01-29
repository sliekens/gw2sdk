using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Finishers;

[PublicAPI]
[DataTransferObject]
public sealed record UnlockedFinisher
{
    public required int Id { get; init; }

    public required bool Permanent { get; init; }

    public required int? Quantity { get; init; }
}
