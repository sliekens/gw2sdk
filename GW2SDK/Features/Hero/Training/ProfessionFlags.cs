namespace GuildWars2.Hero.Training;

/// <summary>Modifiers for professions.</summary>
[PublicAPI]
public sealed record ProfessionFlags : Flags
{
    /// <summary>Whether the profession can swap weapons while in combat.</summary>
    public required bool NoWeaponSwap { get; init; }

    /// <summary>Whether the profession has access to skills linked to race.</summary>
    public required bool NoRacialSkills { get; init; }
}
