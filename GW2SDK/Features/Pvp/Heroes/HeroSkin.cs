using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Heroes;

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
