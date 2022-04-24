using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public enum GameType
{
    Activity = 1,

    Dungeon,

    Pve,

    Pvp,

    PvpLobby,

    Wvw
}
