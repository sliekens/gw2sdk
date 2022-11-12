using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record Trait
{
    public required int Id { get; init; }

    public required int Tier { get; init; }

    public required int Order { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required TraitSlot Slot { get; init; }

    public required IReadOnlyCollection<TraitFact>? Facts { get; init; }

    public required IReadOnlyCollection<CompoundTraitFact>? TraitedFacts { get; init; }

    public required IReadOnlyCollection<TraitSkill>? Skills { get; init; }

    public required int SpezializationId { get; init; }

    public required string Icon { get; init; }
}
