using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public enum GameType
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(GameType)
        Activity = 1,

        Dungeon,

        Pve,

        Pvp,

        PvpLobby,

        Wvw
    }
}
