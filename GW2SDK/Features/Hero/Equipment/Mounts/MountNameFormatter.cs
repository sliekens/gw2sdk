namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>Formats mount names for use in API requests.</summary>
[PublicAPI]
public static class MountNameFormatter
{
    /// <summary>Formats a mount name for use in a request.</summary>
    /// <param name="mountName">Self-explanatory.</param>
    /// <returns>The mount name formatted as a string.</returns>
    /// <exception cref="NotSupportedException">Could not format that mount name.</exception>
    public static string FormatMountName(in Extensible<MountName> mountName)
    {
        return mountName.ToString() switch
        {
            "Griffon" => "griffon",
            "Jackal" => "jackal",
            "Raptor" => "raptor",
            "RollerBeetle" => "roller_beetle",
            "Skimmer" => "skimmer",
            "Skyscale" => "skyscale",
            "Springer" => "springer",
            "Warclaw" => "warclaw",
            "Skiff" => "skiff",
            "SiegeTurtle" => "turtle",
            _ => mountName.ToString()
        };
    }
}
