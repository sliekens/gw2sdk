using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record Transmutation : Consumable
{
    public required IReadOnlyCollection<int> Skins { get; init; }
}
