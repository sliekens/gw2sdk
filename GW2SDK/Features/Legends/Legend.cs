using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Legends;

[PublicAPI]
[DataTransferObject]
public sealed record Legend
{
    public required string Id { get; init; }

    public required int Code { get; init; }

    public required int Swap { get; init; }

    public required int Heal { get; init; }

    public required int Elite { get; init; }

    public required IReadOnlyCollection<int> Utilities { get; init; }
}
