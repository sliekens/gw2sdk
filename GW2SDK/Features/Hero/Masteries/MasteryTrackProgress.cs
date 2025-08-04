namespace GuildWars2.Hero.Masteries;

/// <summary>Information about mastery track progress on the account.</summary>
[DataTransferObject]
public sealed record MasteryTrackProgress
{
    /// <summary>The mastery track ID.</summary>
    public required int Id { get; init; }

    /// <summary>The account's current <see cref="MasteryTrack" /> progress, expressed as a 0-based index that corresponds to
    /// an entry in the <see cref="MasteryTrack.Masteries" /> list.</summary>
    public required int Level { get; init; }
}
