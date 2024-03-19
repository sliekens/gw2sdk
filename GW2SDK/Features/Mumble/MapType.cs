using System.ComponentModel;

namespace GuildWars2.Mumble;

/// <summary>The map types used in the MumbleLink API.</summary>
[PublicAPI]
[DefaultValue(Redirect)]
public enum MapType : uint
{
    Redirect = 0,

    CharacterCreation = 1,

    CompetitivePvP = 2,

    GvG = 3,

    Instance = 4,

    Public = 5,

    Tournament = 6,

    Tutorial = 7,

    UserTournament = 8,

    Center = 9,

    BlueHome = 10,

    GreenHome = 11,

    RedHome = 12,

    FortunesVale = 13,

    ObsidianSanctum = 14,

    EdgeOfTheMists = 15,

    PublicMini = 16,

    BigBattle = 17,

    WvwLounge = 18,

    Wvw = 19
}
