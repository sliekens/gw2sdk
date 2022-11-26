using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Pvp.Heroes;

[PublicAPI]
[DataTransferObject]
public sealed record HeroSkin
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Icon { get; init; }

    public required bool Default { get; init; }

    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }
}
