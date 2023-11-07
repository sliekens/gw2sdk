namespace GuildWars2.Hero.Accounts;

/// <summary>Information about purchased Fractal augmentations and luck on the account.</summary>
[PublicAPI]
public static class ProgressionKind
{
    /// <summary>The account's Agony Impedance level. Each level grants +5 agony resistance.</summary>
    public const string FractalAgonyImpedance = "fractal_agony_impedance";

    /// <summary>The account's Fractal Empowerment level. Each level grants one bonus Writ of Tyrian Mastery experience from
    /// the final chest.</summary>
    public const string FractalEmpowerment = "fractal_empowerment";

    /// <summary>The account's Fractal level. Each level grants one bonus karma item from the final chest.</summary>
    public const string FractalKarmicRetribution = "fractal_karmic_retribution";

    /// <summary>The account's Mist Attunement level. Each level grants a title, permanent increased outgoing damage, health
    /// regeneration and one bonus fractal encryption and relics from the final chest.</summary>
    public const string FractalMistAttunement = "fractal_mist_attunement";

    /// <summary>The account's luck consumed.</summary>
    public const string Luck = "luck";
}
