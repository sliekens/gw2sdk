namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>Formats mount names for use in API requests.</summary>
[PublicAPI]
public static class MountNameFormatter
{
    /// <summary>Formats a mount name for use in a request.</summary>
    /// <param name="mountName">Self-explanatory.</param>
    /// <returns>The mount name formatted as a string.</returns>
    /// <exception cref="NotSupportedException">Could not format that mount name.</exception>
    public static string FormatMountName(MountName mountName) =>
        mountName switch
        {
            MountName.Griffon => "griffon",
            MountName.Jackal => "jackal",
            MountName.Raptor => "raptor",
            MountName.RollerBeetle => "roller_beetle",
            MountName.Skimmer => "skimmer",
            MountName.Skyscale => "skyscale",
            MountName.Springer => "springer",
            MountName.Warclaw => "warclaw",
            MountName.Skiff => "skiff",
            MountName.SiegeTurtle => "turtle",
            _ => throw new NotSupportedException("Could not format mount name.")
        };
}
