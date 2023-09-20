namespace GuildWars2.Traits;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record BuffTraitFact : TraitFact
{
    /// <summary>The duration of the effect applied by the trait, or null when the effect is removed by the trait.</summary>
    public required TimeSpan? Duration { get; init; }

    public required string Status { get; init; }

    public required string Description { get; init; }

    /// <summary>The number of stacks applied by the trait, or null when the effect is removed by the trait.</summary>
    public required int? ApplyCount { get; init; }
}
