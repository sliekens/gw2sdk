using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record AllWorlds
{
    public required IReadOnlyCollection<int> Red { get; init; }

    public required IReadOnlyCollection<int> Blue { get; init; }

    public required IReadOnlyCollection<int> Green { get; init; }
}
