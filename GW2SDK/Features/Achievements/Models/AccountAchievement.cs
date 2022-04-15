using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
[DataTransferObject]
public sealed record AccountAchievement
{
    public int Id { get; init; }

    public IReadOnlyCollection<int>? Bits { get; init; }

    /// <summary>The current number of things completed.
    /// <example>The number of things already killed for a Slayer achievement.</example>
    /// </summary>
    /// <remarks>This can be greater than <see cref="Max" /> but there are no rewards for any additional progress.</remarks>
    public int Current { get; init; }

    /// <summary>The total number of things required to complete the achievement.
    /// <example>The total number of kills required for a Slayer achievement.</example>
    /// </summary>
    /// <remarks>This can be less than <see cref="Current" /> but there are no rewards for any additional progress.</remarks>
    public int Max { get; init; }

    public bool Done { get; init; }

    public int? Repeated { get; init; }

    public bool? Unlocked { get; init; }
}
