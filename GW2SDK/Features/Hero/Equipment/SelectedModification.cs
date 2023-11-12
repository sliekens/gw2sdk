namespace GuildWars2.Hero.Equipment;

// TODO: use modern stat names

/// <summary>A combination of item stats.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedModification
{
    /// <summary>The Agony Resistance attribute modifier.</summary>
    public required int? AgonyResistance { get; init; }

    /// <summary>The Concentration attribute modifier.</summary>
    public required int? BoonDuration { get; init; }

    /// <summary>The Condition Damage attribute modifier.</summary>
    public required int? ConditionDamage { get; init; }

    /// <summary>The Expertise attribute modifier.</summary>
    public required int? ConditionDuration { get; init; }

    /// <summary>The Ferocity attribute modifier.</summary>
    public required int? CritDamage { get; init; }

    /// <summary>The Healing Power attribute modifier.</summary>
    public required int? Healing { get; init; }

    /// <summary>The Power attribute modifier.</summary>
    public required int? Power { get; init; }

    /// <summary>The Precision attribute modifier.</summary>
    public required int? Precision { get; init; }

    /// <summary>The Toughness attribute modifier.</summary>
    public required int? Toughness { get; init; }

    /// <summary>The Vitality attribute modifier.</summary>
    public required int? Vitality { get; init; }
}
