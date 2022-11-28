using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.ItemStats;

/// <summary>A combination of item attributes.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedModification
{
    public required int? AgonyResistance { get; init; }

    public required int? BoonDuration { get; init; }

    public required int? ConditionDamage { get; init; }

    public required int? ConditionDuration { get; init; }

    public required int? CritDamage { get; init; }

    public required int? Healing { get; init; }

    public required int? Power { get; init; }

    public required int? Precision { get; init; }

    public required int? Toughness { get; init; }

    public required int? Vitality { get; init; }
}
