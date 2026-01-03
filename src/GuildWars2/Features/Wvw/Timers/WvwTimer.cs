namespace GuildWars2.Wvw.Timers;

/// <summary>Information about WvW lockout or team assignment dates.</summary>
public sealed record WvwTimer
{
    /// <summary>The date and time when the timer expires for the North American region.</summary>
    public required DateTimeOffset NorthAmerica { get; init; }

    /// <summary>The date and time when the timer expires for the European region.</summary>
    public required DateTimeOffset Europe { get; init; }
}
