namespace GuildWars2;

/// <summary>The permissions that can be assigned to a guild rank.</summary>
[PublicAPI]
public enum GuildPermission
{
    /// <summary>Utilize consumable placeables created via the workshop.</summary>
    ActivatePlaceables = 1,

    /// <summary>Activate guild world events.</summary>
    ActivateWorldEvent,

    /// <summary>Invite new players to the guild. Promote, Demote, Kick current members only from lower ranks.</summary>
    Admin,

    /// <summary>Allowed to activate tactics at a claimable.</summary>
    ClaimableActivate,

    /// <summary>Allowed to claim objectives.</summary>
    ClaimableClaim,

    /// <summary>Allowed to edit options at guild-owned claimables.</summary>
    ClaimableEditOptions,

    /// <summary>Allowed to claim objectives.</summary>
    ClaimableSpend,

    /// <summary>Remove All Decorations.</summary>
    DecorationAdmin,

    /// <summary>Browse and deposit funds into the small guild stash.</summary>
    DepositCoinsStash,

    /// <summary>Browse and deposit funds in the larger guild treasure trove.</summary>
    DepositCoinsTrove,

    /// <summary>Browse and deposit items into the small guild stash.</summary>
    DepositItemsStash,

    /// <summary>Browse and deposit items in the larger guild treasure trove.</summary>
    DepositItemsTrove,

    /// <summary>Change the guild's anthem.</summary>
    EditAnthem,

    /// <summary>Queue and reorder schematics in the workshop assembly.</summary>
    EditAssemblyQueue,

    /// <summary>Change the guild hall's background music.</summary>
    EditBGM,

    /// <summary>Change the appearance of the guild emblem as it appears on all armor, flags, banners, and objects.</summary>
    EditEmblem,

    /// <summary>Ability to place and remove monuments in the guild hall.</summary>
    EditMonument,

    /// <summary>Edit the message of the day which is broadcast to all guild members.</summary>
    EditMOTD,

    /// <summary>Create, delete, and edit the properties of ranks lower than this one.</summary>
    EditRoles,

    /// <summary>Activate guild missions.</summary>
    MissionControl,

    /// <summary>Utilize the Guild Portal for group teleportation.</summary>
    OpenPortal,

    /// <summary>Player can place, move, and remove decorations in the arena.</summary>
    PlaceArenaDecoration,

    /// <summary>Player can place, move, and remove decorations in the guild hall.</summary>
    PlaceDecoration,

    /// <summary>Purchase upgrades for the guild.</summary>
    PurchaseUpgrades,

    /// <summary>Claim a new guild hall for your guild.</summary>
    SetGuildHall,

    /// <summary>Instantly complete in-progress schematics by spending resonance.</summary>
    SpendFuel,

    /// <summary>New members will start at this rank. This permission can only exist on one rank.</summary>
    StartingRole,

    /// <summary>Create or delete teams and add or remove team members.</summary>
    TeamAdmin,

    /// <summary>Withdraw funds from the small guild stash.</summary>
    WithdrawCoinsStash,

    /// <summary>Withdraw gold from the larger guild treasure trove.</summary>
    WithdrawCoinsTrove,

    /// <summary>Withdraw items from the small guild stash.</summary>
    WithdrawItemsStash,

    /// <summary>Withdraw items from the larger guild treasure trove.</summary>
    WithdrawItemsTrove
}
