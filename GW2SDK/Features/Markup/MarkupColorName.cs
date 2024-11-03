using System.Collections.ObjectModel;

namespace GuildWars2.Markup;

/// <summary>The color names used in markup.</summary>
[PublicAPI]
public static class MarkupColorName
{
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

    /// <summary>
    /// A dictionary that maps color names to their corresponding hex color codes, based on colors picked from the game.
    /// </summary>
    /// <remarks>
    /// The dictionary is case-insensitive and contains the following default mappings:
    /// <list type="bullet">
    /// <item><term>@abilitytype</term><description>#ffee88 (light yellow)</description></item>
    /// <item><term>@flavor</term><description>#99dddd (aqua)</description></item>
    /// <item><term>@reminder</term><description>#aaaaaa (gray)</description></item>
    /// <item><term>@task</term><description>#ffcc55 (gold)</description></item>
    /// <item><term>@warning</term><description>#ff0000 (red)</description></item>
    /// </list>
    /// </remarks>
    public static readonly IReadOnlyDictionary<string, string> DefaultColorMap =
        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [AbilityType] = "#ffee88",
            [Flavor] = "#99dddd",
            [Reminder] = "#aaaaaa",
            [Task] = "#ffcc55",
            [Warning] = "#ff0000"
        });

    /// <summary>Determines whether the specified color name is defined.</summary>
    /// <remarks>The name should include the leading '@' symbol, e.g. '@flavor'.</remarks>
    /// <param name="colorName">The name of the color to check.</param>
    /// <returns><c>true</c> if the specified color name is defined; otherwise, <c>false</c>.</returns>
    public static bool IsDefined(string colorName)
    {
        return DefaultColorMap.ContainsKey(colorName);
    }
}
