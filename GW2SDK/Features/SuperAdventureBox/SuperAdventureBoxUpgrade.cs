using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxUpgrade
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}
