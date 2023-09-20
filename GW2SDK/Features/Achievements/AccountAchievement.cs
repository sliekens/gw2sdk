namespace GuildWars2.Achievements;

[PublicAPI]
[DataTransferObject]
public sealed record AccountAchievement
{
    public required int Id { get; init; }

    public required IReadOnlyCollection<int>? Bits { get; init; }

    /// <summary>The current number of things completed.</summary>
    /// <example>The number of things already killed for a Slayer achievement.</example>
    /// <remarks>This can be greater than <see cref="Max" /> but there are no rewards for any additional progress.</remarks>
    public required int Current { get; init; }

    /// <summary>The total number of things required to complete the achievement.</summary>
    /// <example>The total number of kills required for a Slayer achievement.</example>
    /// <remarks>This can be less than <see cref="Current" /> but there are no rewards for any additional progress.</remarks>
    public required int Max { get; init; }

    public required bool Done { get; init; }

    public required int? Repeated { get; init; }

    public required bool? Unlocked { get; init; }
}
