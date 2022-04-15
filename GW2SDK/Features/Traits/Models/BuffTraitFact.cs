using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits.Models;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record BuffTraitFact : TraitFact
{
    /// <summary>The duration of the effect applied by the trait, or null when the effect is removed by the trait.</summary>
    public TimeSpan? Duration { get; init; }

    public string Status { get; init; } = "";

    public string Description { get; init; } = "";

    /// <summary>The number of stacks applied by the trait, or null when the effect is removed by the trait.</summary>
    public int? ApplyCount { get; init; }
}
