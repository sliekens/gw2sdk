namespace GuildWars2.Hero.Equipment;


/// <summary>A combination of item attributes like Power, Precision, Ferocity.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AttributeCombination
{
    /// <summary>The Agony Resistance attribute modifier.</summary>
    public required int? AgonyResistance { get; init; }

    /// <summary>The Concentration attribute modifier.</summary>
    public required int? Concentration { get; init; }

    /// <summary>The Condition Damage attribute modifier.</summary>
    public required int? ConditionDamage { get; init; }

    /// <summary>The Expertise attribute modifier.</summary>
    public required int? Expertise { get; init; }

    /// <summary>The Ferocity attribute modifier.</summary>
    public required int? Ferocity { get; init; }

    /// <summary>The Healing Power attribute modifier.</summary>
    public required int? HealingPower { get; init; }

    /// <summary>The Power attribute modifier.</summary>
    public required int? Power { get; init; }

    /// <summary>The Precision attribute modifier.</summary>
    public required int? Precision { get; init; }

    /// <summary>The Toughness attribute modifier.</summary>
    public required int? Toughness { get; init; }

    /// <summary>The Vitality attribute modifier.</summary>
    public required int? Vitality { get; init; }
}
