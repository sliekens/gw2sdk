using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

/// <summary>Modifiers for achievements.</summary>
[PublicAPI]
[JsonConverter(typeof(AchievementFlagsJsonConverter))]
public sealed record AchievementFlags : Flags
{
    /// <summary>The achievement is a meta-achievements like End of Dragons: Act 1 Mastery. This flag inherits the
    /// <see cref="MoveToTop" /> flag.</summary>
    public required bool CategoryDisplay { get; init; }

    /// <summary>The achievement progress is reset every day at midnight UTC.</summary>
    public required bool Daily { get; init; }

    /// <summary>The achievement does not appear in the achievement panel until you start progressing or complete it.</summary>
    public required bool Hidden { get; init; }

    /// <summary>The achievement is never displayed in the Nearly Complete section of the achievement panel.</summary>
    public required bool IgnoreNearlyComplete { get; init; }

    /// <summary>The achievement is sorted before achievements without this flag. Its name is displayed in the color of the
    /// achievement category.</summary>
    public required bool MoveToTop { get; init; }

    /// <summary>The achievement can be progressed while in PvP or WvW competitive modes. Otherwise progress is not counted.</summary>
    public required bool Pvp { get; init; }

    /// <summary>Internal flag for achievements that are revalidated on login.</summary>
    public required bool RepairOnLogin { get; init; }

    /// <summary>The achievement can be completed multiple times without a time-gate.</summary>
    public required bool Repeatable { get; init; }

    /// <summary>The achievement is locked by default.</summary>
    public required bool RequiresUnlock { get; init; }

    /// <summary>The achievement progress is never reset.</summary>
    public required bool Permanent { get; init; }

    /// <summary>The achievement progress is reset every week on Monday at 7:30am UTC.</summary>
    public required bool Weekly { get; init; }
}
