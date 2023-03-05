using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Banking;

[PublicAPI]
[DataTransferObject]
public sealed record MaterialSlot
{
    public required int ItemId { get; init; }

    public required int CategoryId { get; init; }

    public required ItemBinding Binding { get; init; }

    public required int Count { get; init; }
}
