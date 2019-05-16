namespace GW2SDK.Features.Tokens
{
    public enum Permission
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(Permission)
        Account = 1,

        Builds,

        Characters,

        Guilds,

        Inventories,

        Progression,

        PvP,

        Unlocks,

        Wallet,

        TradingPost
    }
}