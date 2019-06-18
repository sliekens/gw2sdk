﻿using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    [PublicAPI]
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