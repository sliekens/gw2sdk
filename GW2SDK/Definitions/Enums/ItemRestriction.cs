using System.ComponentModel;

namespace GuildWars2;

/// <summary>A list of possible restrictions for items that are limited to some races, genders or professions.</summary>
[PublicAPI]
[DefaultValue(None)]
public enum ItemRestriction
{
    None,

    #region Races

    Asura,

    Charr,

    Human,

    Norn,

    Sylvari,

    #endregion Races

    #region Genders

    Female,

    Male,

    #endregion Genders

    #region Professions

    Guardian,

    Warrior,

    Engineer,

    Ranger,

    Thief,

    Elementalist,

    Mesmer,

    Necromancer,

    Revenant

    #endregion Professions
}
