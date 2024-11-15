using System.Text.Json.Serialization;
using GuildWars2.Chat;

namespace GuildWars2.Items;

/// <summary>Information about an item. This type is the base type for all items. Cast objects of this type to a more
/// specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
[JsonPolymorphic]
[JsonDerivedType(typeof(Item), "base")]
[JsonDerivedType(typeof(CraftingMaterial), "crafting-material")]
[JsonDerivedType(typeof(JadeTechModule), "jade-tech-module")]
[JsonDerivedType(typeof(PowerCore), "power-core")]
[JsonDerivedType(typeof(Bag), "bag")]
[JsonDerivedType(typeof(Relic), "relic")]
[JsonDerivedType(typeof(Miniature), "miniature")]
[JsonDerivedType(typeof(Trophy), "trophy")]
[JsonDerivedType(typeof(Backpack), "back")]
[JsonDerivedType(typeof(SalvageTool), "salvage-tool")]
[JsonDerivedType(typeof(Container), "container")]
[JsonDerivedType(typeof(ImmediateContainer), "immediate-container")]
[JsonDerivedType(typeof(BlackLionChest), "black-lion-chest")]
[JsonDerivedType(typeof(GiftBox), "gift-box")]
[JsonDerivedType(typeof(UpgradeComponent), "upgrade-component")]
[JsonDerivedType(typeof(Sigil), "sigil")]
[JsonDerivedType(typeof(Gem), "gem")]
[JsonDerivedType(typeof(Rune), "rune")]
[JsonDerivedType(typeof(Trinket), "trinket")]
[JsonDerivedType(typeof(Ring), "ring")]
[JsonDerivedType(typeof(Amulet), "amulet")]
[JsonDerivedType(typeof(Accessory), "accessory")]
[JsonDerivedType(typeof(Armor), "armor")]
[JsonDerivedType(typeof(Helm), "helm")]
[JsonDerivedType(typeof(HelmAquatic), "helm-aquatic")]
[JsonDerivedType(typeof(Shoulders), "shoulders")]
[JsonDerivedType(typeof(Coat), "coat")]
[JsonDerivedType(typeof(Gloves), "gloves")]
[JsonDerivedType(typeof(Leggings), "leggings")]
[JsonDerivedType(typeof(Boots), "boots")]
[JsonDerivedType(typeof(Weapon), "weapon")]
[JsonDerivedType(typeof(Greatsword), "greatsword")]
[JsonDerivedType(typeof(Torch), "torch")]
[JsonDerivedType(typeof(Staff), "staff")]
[JsonDerivedType(typeof(HarpoonGun), "harpoon-gun")]
[JsonDerivedType(typeof(Longbow), "longbow")]
[JsonDerivedType(typeof(Shortbow), "shortbow")]
[JsonDerivedType(typeof(SmallBundle), "small-bundle")]
[JsonDerivedType(typeof(LargeBundle), "large-bundle")]
[JsonDerivedType(typeof(Pistol), "pistol")]
[JsonDerivedType(typeof(Mace), "mace")]
[JsonDerivedType(typeof(Rifle), "rifle")]
[JsonDerivedType(typeof(Spear), "spear")]
[JsonDerivedType(typeof(Dagger), "dagger")]
[JsonDerivedType(typeof(Scepter), "scepter")]
[JsonDerivedType(typeof(Sword), "sword")]
[JsonDerivedType(typeof(Trident), "trident")]
[JsonDerivedType(typeof(Hammer), "hammer")]
[JsonDerivedType(typeof(Warhorn), "warhorn")]
[JsonDerivedType(typeof(Focus), "focus")]
[JsonDerivedType(typeof(Toy), "toy")]
[JsonDerivedType(typeof(ToyTwoHanded), "toy-two-handed")]
[JsonDerivedType(typeof(Axe), "axe")]
[JsonDerivedType(typeof(Shield), "shield")]
[JsonDerivedType(typeof(GatheringTool), "gathering-tool")]
[JsonDerivedType(typeof(Lure), "lure")]
[JsonDerivedType(typeof(Bait), "bait")]
[JsonDerivedType(typeof(MiningPick), "mining-pick")]
[JsonDerivedType(typeof(HarvestingSickle), "harvesting-sickle")]
[JsonDerivedType(typeof(LoggingAxe), "logging-axe")]
[JsonDerivedType(typeof(Gizmo), "gizmo")]
[JsonDerivedType(typeof(BlackLionChestKey), "black-lion-chest-key")]
[JsonDerivedType(typeof(UnlimitedConsumable), "unlimited-consumable")]
[JsonDerivedType(typeof(RentableContractNpc), "rentable-contract-npc")]
[JsonDerivedType(typeof(Consumable), "consumable")]
[JsonDerivedType(typeof(Food), "food")]
[JsonDerivedType(typeof(Utility), "utility")]
[JsonDerivedType(typeof(Transmutation), "transmutation")]
[JsonDerivedType(typeof(GenericConsumable), "generic-consumable")]
[JsonDerivedType(typeof(Currency), "currency")]
[JsonDerivedType(typeof(Booze), "booze")]
[JsonDerivedType(typeof(ContractNpc), "contract-npc")]
[JsonDerivedType(typeof(MountLicense), "mount-license")]
[JsonDerivedType(typeof(UpgradeExtractor), "upgrade-extractor")]
[JsonDerivedType(typeof(Service), "service")]
[JsonDerivedType(typeof(RandomUnlocker), "random-unlocker")]
[JsonDerivedType(typeof(HalloweenConsumable), "halloween-consumable")]
[JsonDerivedType(typeof(TeleportToFriend), "teleport-to-friend")]
[JsonDerivedType(typeof(AppearanceChanger), "appearance-changer")]
[JsonDerivedType(typeof(Unlocker), "unlocker")]
[JsonDerivedType(typeof(RecipeSheet), "recipe-sheet")]
[JsonDerivedType(typeof(BankTabExpansion), "bank-tab-expansion")]
[JsonDerivedType(typeof(BagSlotExpansion), "bag-slot-expansion")]
[JsonDerivedType(typeof(EquipmentTemplateExpansion), "equipment-template-expansion")]
[JsonDerivedType(typeof(BuildTemplateExpansion), "build-template-expansion")]
[JsonDerivedType(typeof(BuildStorageExpansion), "build-storage-expansion")]
[JsonDerivedType(typeof(Dye), "dye")]
[JsonDerivedType(typeof(StorageExpander), "storage-expander")]
[JsonDerivedType(typeof(ContentUnlocker), "content-unlocker")]
[JsonDerivedType(typeof(OutfitUnlocker), "outfit-unlocker")]
[JsonDerivedType(typeof(GliderSkinUnlocker), "glider-skin-unlocker")]
[JsonDerivedType(typeof(MountSkinUnlocker), "mount-skin-unlocker")]
[JsonDerivedType(typeof(MiniatureUnlocker), "miniature-unlocker")]
[JsonDerivedType(typeof(SharedInventorySlot), "shared-inventory-slot")]
[JsonDerivedType(typeof(MistChampionSkinUnlocker), "mist-champion-skin-unlocker")]
[JsonDerivedType(typeof(JadeBotSkinUnlocker), "jade-bot-skin-unlocker")]
public record Item
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the item.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the item as it appears in the tooltip.</summary>
    public required string Description { get; init; }

    /// <summary>The minimum level required to use the item.</summary>
    public required int Level { get; init; }

    /// <summary>The item rarity.</summary>
    public required Extensible<Rarity> Rarity { get; init; }

    /// <summary>The unit price of the item when sold to a merchant. Items will not appear in a sell-to-vendor list when this
    /// value is <see cref="Coin.Zero" />, or when the <see cref="ItemFlags.NoSell" /> flag is set.</summary>
    public required Coin VendorValue { get; init; }

    /// <summary>The game types in which the items can be used.</summary>
    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<Extensible<GameType>> GameTypes { get; init; }

    /// <summary>Contains various modifiers.</summary>
    public required ItemFlags Flags { get; init; }

    /// <summary>The character restrictions for the item.</summary>
    public required ItemRestriction Restrictions { get; init; }

    /// <summary>The chat code of the item. This can be used to link the item in the chat, but also in guild or squad messages.</summary>
    public required string ChatLink { get; init; }

    /// <summary>The URL of the item icon.</summary>
    public required string? IconHref { get; init; }

    /// <summary>Gets a chat link object for this item.</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink GetChatLink() =>
        new()
        {
            ItemId = Id,
            Count = 1,
            SkinId = null,
            SuffixItemId = null,
            SecondarySuffixItemId = null
        };
}
