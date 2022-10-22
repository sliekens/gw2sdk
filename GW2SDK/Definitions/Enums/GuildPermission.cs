using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public enum GuildPermission
{
    ActivatePlaceables = 1,

    ActivateWorldEvent,

    Admin,

    ClaimableActivate,

    ClaimableClaim,

    ClaimableEditOptions,

    ClaimableSpend,

    DecorationAdmin,

    DepositCoinsStash,

    DepositCoinsTrove,

    DepositItemsStash,

    DepositItemsTrove,

    EditAnthem,

    EditAssemblyQueue,

    EditBGM,

    EditEmblem,

    EditMonument,

    EditMOTD,

    EditRoles,

    MissionControl,

    OpenPortal,

    PlaceArenaDecoration,

    PlaceDecoration,

    PurchaseUpgrades,

    SetGuildHall,

    SpendFuel,

    StartingRole,

    TeamAdmin,

    WithdrawCoinsStash,

    WithdrawCoinsTrove,

    WithdrawItemsStash,

    WithdrawItemsTrove
}
