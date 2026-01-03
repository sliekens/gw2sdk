using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Titles;

/// <summary>Information about a title which can be obtained from achievements.</summary>
[DataTransferObject]
[JsonConverter(typeof(TitleJsonConverter))]
public sealed record Title
{
    /// <summary>The title ID.</summary>
    public required int Id { get; init; }

    /// <summary>The title itself.</summary>
    public required string Name { get; init; }

    /// <summary>A list of achievement IDs which award this title. Can be null if the title is awarded by reaching some amount
    /// of points instead.</summary>
    /// <remarks>This is usually a single achievement, but some titles are awarded by multiple achievements.</remarks>
    public required IReadOnlyList<int>? Achievements { get; init; }

    /// <summary>The amount of points which will grant this title if it is not an achievement reward.</summary>
    public required int? AchievementPointsRequired { get; init; }
}
