using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace GuildWars2;

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
