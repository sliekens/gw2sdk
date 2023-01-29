using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record Encounter
{
    public required string Id { get; init; }

    public required EncounterKind Kind { get; init; }
}
