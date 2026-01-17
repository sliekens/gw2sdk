using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

/// <summary>Information about achievement progress made on the account.</summary>
[DataTransferObject]
[JsonConverter(typeof(AccountAchievementJsonConverter))]
public sealed record AccountAchievement
{
    /// <summary>The achievement ID.</summary>
    public required int Id { get; init; }

    /// <summary>The IDs of the achievement bits that have been completed.</summary>
    public required IImmutableValueList<int>? Bits { get; init; }

    /// <summary>The current number of things completed. Context is required to understand this number. For example if it is a
    /// Slayer achievement, this would be the number of things already slayed.</summary>
    /// <remarks>This can be greater than <see cref="Max" /> but there are no rewards for any additional progress.</remarks>
    public required int Current { get; init; }

    /// <summary>The total number of things required to complete the achievement. Context is required to understand this
    /// number. For example if it is a Slayer achievement, this would be the total number of kills required.</summary>
    /// <remarks>This can be less than <see cref="Current" /> but there are no rewards for any additional progress.</remarks>
    public required int Max { get; init; }

    /// <summary>Whether the achievement has been completed.</summary>
    public required bool Done { get; init; }

    /// <summary>How many times the achievement has been repeated.</summary>
    public required int Repeated { get; init; }

    /// <summary>Whether the achievement has been unlocked for the account.</summary>
    public required bool Unlocked { get; init; }
}
