using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxProgress

{
    public required IReadOnlyCollection<SuperAdventureBoxZone> Zones { get; init; }

    public required IReadOnlyCollection<SuperAdventureBoxUpgrade> Unlocks { get; init; }

    public required IReadOnlyCollection<SuperAdventureBoxSong> Songs { get; init; }
}
