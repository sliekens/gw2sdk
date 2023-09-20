namespace GuildWars2;

/// <summary>A list of possible restrictions for items that are limited to some races, genders or professions.</summary>
[PublicAPI]
public enum ItemRestriction
{
    #region Races

    Asura = 1,

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

    Guardian = 1,

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
