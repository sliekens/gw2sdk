namespace GuildWars2.Hero.Training;

/// <summary>Modifiers for professions.</summary>
[PublicAPI]
public sealed record ProfessionFlags
{
    /// <summary>Whether the profession can swap weapons while in combat.</summary>
    public required bool NoWeaponSwap { get; init; }

    /// <summary>Whether the profession has access to skills linked to race.</summary>
    public required bool NoRacialSkills { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
