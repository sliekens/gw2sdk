using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxZone
{
    public required int Id { get; init; }

    public required SuperAdventureBoxMode Mode { get; init; }

    public required int World { get; init; }

    public required int Zone { get; init; }
}
