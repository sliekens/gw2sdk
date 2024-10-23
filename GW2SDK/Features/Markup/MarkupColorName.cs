using static System.StringComparison;

namespace GuildWars2.Markup;

/// <summary>The color names used in markup.</summary>
[PublicAPI]
public static class MarkupColorName
{
    /// <summary>The color for flavor text.</summary>
    public static string Flavor => "@flavor";

    /// <summary>The color for reminder text.</summary>
    public static string Reminder => "@reminder";

    /// <summary>The color for ability type text.</summary>
    public static string AbilityType => "@abilitytype";

    /// <summary>The color for warning text.</summary>
    public static string Warning => "@warning";

    /// <summary>The color for task text.</summary>
    public static string Task => "@task";

    /// <summary>Determines whether the specified color name is defined.</summary>
    /// <remarks>The name should include the leading '@' symbol, e.g. '@flavor'.</remarks>
    /// <param name="colorName">The name of the color to check.</param>
    /// <returns><c>true</c> if the specified color name is defined; otherwise, <c>false</c>.</returns>
    public static bool IsDefined(string colorName)
    {
        return string.Equals(colorName, Flavor, OrdinalIgnoreCase)
            || string.Equals(colorName, Reminder, OrdinalIgnoreCase)
            || string.Equals(colorName, AbilityType, OrdinalIgnoreCase)
            || string.Equals(colorName, Warning, OrdinalIgnoreCase)
            || string.Equals(colorName, Task, OrdinalIgnoreCase);
    }
}
