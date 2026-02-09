using System.Collections.ObjectModel;

namespace GuildWars2.Markup;

/// <summary>The color names used in markup.</summary>
public static class MarkupColorName
{
    /// <summary>A dictionary that maps color names to their corresponding hex color codes, based on colors picked from the game.</summary>
    /// <remarks>
    /// The dictionary is case-insensitive and contains the following default mappings:
    /// <list type="bullet">
    ///   <item>
    ///     <term>@abilitytype</term>
    ///     <description>#ffec8c (light yellow)</description>
    ///   </item>
    ///   <item>
    ///     <term>@flavor</term>
    ///     <description>#9be8e4 (aqua)</description>
    ///   </item>
    ///   <item>
    ///     <term>@reminder</term>
    ///     <description>#b0b0b0 (gray)</description>
    ///   </item>
    ///   <item>
    ///     <term>@quest</term>
    ///     <description>#00ff00 (green)</description>
    ///   </item>
    ///   <item>
    ///     <term>@task</term>
    ///     <description>#ffc957 (gold)</description>
    ///   </item>
    ///   <item>
    ///     <term>@warning</term>
    ///     <description>#ed0002 (red)</description>
    ///   </item>
    ///   <item>
    ///     <term>@event</term>
    ///     <description>#cc6633 (orange)</description>
    ///   </item>
    ///   <item>
    ///     <term>@info</term>
    ///     <description>#ffffff (white)</description>
    ///   </item>
    /// </list>
    /// </remarks>
    public static readonly IReadOnlyDictionary<string, string> DefaultColorMap =
        new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                [AbilityType] = "#ffec8c",
                [Flavor] = "#9be8e4",
                [Reminder] = "#b0b0b0",
                [Quest] = "#00ff00",
                [Task] = "#ffc957",
                [Warning] = "#ed0002",
                [Event] = "#cc6633",
                [Info] = "#ffffff"
            }
        );

    /// <summary>The color for flavor text. Color used in game: Aqua.</summary>
    public static string Flavor => "@flavor";

    /// <summary>The color for reminder text. Color used in game: Gray.</summary>
    public static string Reminder => "@reminder";

    /// <summary>The color for ability type text. Color used in game: Light Yellow.</summary>
    public static string AbilityType => "@abilitytype";

    /// <summary>The color for warning text. Color used in game: Red.</summary>
    public static string Warning => "@warning";

    /// <summary>The color for task text. Color used in game: Gold.</summary>
    public static string Task => "@task";

    /// <summary>The color for info text. Color used in game: White.</summary>
    public static string Info => "@info";

    /// <summary>The color for quest text. Color used in game: Green.</summary>
    public static string Quest => "@quest";

    /// <summary>The color for event text. Color used in game: Orange.</summary>
    public static string Event => "@event";

    /// <summary>Determines whether the specified color name is defined.</summary>
    /// <remarks>The name should include the leading '@' symbol, e.g. '@flavor'.</remarks>
    /// <param name="colorName">The name of the color to check.</param>
    /// <returns><c>true</c> if the specified color name is defined; otherwise, <c>false</c>.</returns>
    public static bool IsDefined(string colorName)
    {
        return DefaultColorMap.ContainsKey(colorName);
    }
}
