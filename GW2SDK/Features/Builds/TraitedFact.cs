namespace GuildWars2.Builds;

/// <summary>A fact applied by the skill/trait when another trait is active.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record TraitedFact
{
    /// <summary>The ID of the trait that must be active to benefits from this <see cref="Fact" />.</summary>
    public required int RequiresTrait { get; init; }

    /// <summary>If present, it is the list index of an existing fact to override with this <see cref="Fact" />. Otherwise it
    /// should be added to the list without overriding.</summary>
    public required int? Overrides { get; init; }

    /// <summary>The fact which is added or which replaces another fact if <see cref="Overrides" /> is present.</summary>
    public required Fact Fact { get; init; }
}
