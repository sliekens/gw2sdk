using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Items;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

public class Items
{
    [Test]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/items.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach ((Item actual, MessageContext context) in sut.Items.GetItemsBulk(cancellationToken: TestContext.Current!.CancellationToken))
        {
            Assert.NotNull(context);
            Assert.True(actual.Id >= 1);
            Assert.NotNull(actual.Name);
            Assert.NotNull(actual.Description);
            MarkupSyntaxValidator.Validate(actual.Description);
            Assert.True(actual.Rarity.IsDefined());
            Assert.True(actual.VendorValue.Amount >= 0);
            Assert.InRange(actual.Level, 0, 80);
            Assert.NotEmpty(actual.Restrictions.BodyTypes);
            Assert.All(actual.Restrictions.BodyTypes, type => Assert.True(type.IsDefined()));
            Assert.NotEmpty(actual.Restrictions.Races);
            Assert.All(actual.Restrictions.Races, race => Assert.True(race.IsDefined()));
            Assert.NotEmpty(actual.Restrictions.Professions);
            Assert.Empty(actual.Restrictions.Other);
            Assert.All(actual.Restrictions.Professions, profession => Assert.True(profession.IsDefined()));
            Assert.All(actual.GameTypes, gameType => Assert.True(gameType.IsDefined()));
            switch (actual)
            {
                case Consumable consumable:
                    switch (consumable)
                    {
                        case Transmutation transmutation:
                            Assert.NotEmpty(transmutation.SkinIds);
                            break;
                        case GenericConsumable genericConsumable:
                            if (genericConsumable.Effect is not null)
                            {
                                // Effect.Name and Description can be empty, but should not be null
                                Assert.NotNull(genericConsumable.Effect.Name);
                                Assert.NotNull(genericConsumable.Effect.Description);
                                Assert.True(genericConsumable.Effect.Duration >= TimeSpan.Zero);
                                Assert.True(genericConsumable.Effect.ApplyCount >= 0);
                            }

                            if (genericConsumable.GuildUpgradeId.HasValue)
                            {
                                Assert.True(genericConsumable.GuildUpgradeId.Value > 0);
                            }

                            break;
                        case Utility utility:
                            if (utility.Effect is not null)
                            {
                                // Effect.Name and Description can be empty, but should not be null
                                Assert.NotNull(utility.Effect.Name);
                                Assert.NotNull(utility.Effect.Description);
                                Assert.True(utility.Effect.Duration >= TimeSpan.Zero);
                                Assert.True(utility.Effect.ApplyCount >= 0);
                            }

                            break;
                        case Food food:
                            if (food.Effect is not null)
                            {
                                // Effect.Name and Description can be empty, but should not be null
                                Assert.NotNull(food.Effect.Name);
                                Assert.NotNull(food.Effect.Description);
                                Assert.True(food.Effect.Duration >= TimeSpan.Zero);
                                Assert.True(food.Effect.ApplyCount >= 0);
                            }

                            break;
                        case Service service:
                            if (service.Effect is not null)
                            {
                                // Effect.Name and Description can be empty, but should not be null
                                Assert.NotNull(service.Effect.Name);
                                Assert.NotNull(service.Effect.Description);
                                Assert.True(service.Effect.Duration >= TimeSpan.Zero);
                                Assert.True(service.Effect.ApplyCount >= 0);
                            }

                            if (service.GuildUpgradeId.HasValue)
                            {
                                Assert.True(service.GuildUpgradeId.Value > 0);
                            }

                            break;
                        case Booze booze:
                            // Nothing specific to verify for Booze items
                            break;
                        case ContractNpc contractNpc:
                            // Nothing specific to verify for ContractNpc items
                            break;
                        case HalloweenConsumable halloweenConsumable:
                            // Nothing specific to verify for HalloweenConsumable items
                            break;
                        case MountLicense mountLicense:
                            // Nothing specific to verify for MountLicense items
                            break;
                        case TeleportToFriend teleportToFriend:
                            // Nothing specific to verify for TeleportToFriend items
                            break;
                        case Unlocker unlocker:
                            switch (unlocker)
                            {
                                case RecipeSheet recipeSheet:
                                    Assert.True(recipeSheet.RecipeId > 0);
                                    Assert.NotNull(recipeSheet.ExtraRecipeIds);
                                    foreach ((int extraRecipeId, RecipeLink? extraRecipeLink) in recipeSheet.ExtraRecipeIds.Zip(recipeSheet.GetExtraRecipeChatLinks(), (extraRecipeId, extraRecipeLink) => (extraRecipeId, extraRecipeLink)))
                                    {
                                        Assert.Equal(extraRecipeId, extraRecipeLink.RecipeId);
                                    }

                                    RecipeLink sheetRecipeLink = recipeSheet.GetRecipeChatLink();
                                    Assert.Equal(recipeSheet.RecipeId, sheetRecipeLink.RecipeId);
                                    RecipeLink sheetRecipeLinkRoundtrip = RecipeLink.Parse(sheetRecipeLink.ToString());
                                    Assert.Equal(sheetRecipeLink.ToString(), sheetRecipeLinkRoundtrip.ToString());
                                    break;
                                case Dye dye:
                                    Assert.True(dye.ColorId > 0);
                                    break;
                                case BagSlotExpansion bagSlotExpansion:
                                    // Nothing specific to verify for BagSlotExpansion items
                                    break;
                                case BankTabExpansion bankTabExpansion:
                                    // Nothing specific to verify for BankTabExpansion items
                                    break;
                                case BuildStorageExpansion buildStorageExpansion:
                                    // Nothing specific to verify for BuildStorageExpansion items
                                    break;
                                case BuildTemplateExpansion buildTemplateExpansion:
                                    // Nothing specific to verify for BuildTemplateExpansion items
                                    break;
                                case ContentUnlocker contentUnlocker:
                                    // Nothing specific to verify for ContentUnlocker items
                                    break;
                                case EquipmentTemplateExpansion equipmentTemplateExpansion:
                                    // Nothing specific to verify for EquipmentTemplateExpansion items
                                    break;
                                case GliderSkinUnlocker gliderSkinUnlocker:
                                    // Nothing specific to verify for GliderSkinUnlocker items
                                    break;
                                case JadeBotSkinUnlocker jadeBotSkinUnlocker:
                                    // Nothing specific to verify for JadeBotSkinUnlocker items
                                    break;
                                case MagicDoorSkinUnlocker:
                                    // Nothing specific to verify for MagicDoorSkinUnlocker items
                                    break;
                                case MiniatureUnlocker miniatureUnlocker:
                                    // Nothing specific to verify for MiniatureUnlocker items
                                    break;
                                case MistChampionSkinUnlocker mistChampionSkinUnlocker:
                                    // Nothing specific to verify for MistChampionSkinUnlocker items
                                    break;
                                case MountSkinUnlocker mountSkinUnlocker:
                                    // Nothing specific to verify for MountSkinUnlocker items
                                    break;
                                case OutfitUnlocker outfitUnlocker:
                                    // Nothing specific to verify for OutfitUnlocker items
                                    break;
                                case SharedInventorySlot sharedInventorySlot:
                                    // Nothing specific to verify for SharedInventorySlot items
                                    break;
                                case StorageExpander storageExpander:
                                    // Nothing specific to verify for StorageExpander items
                                    break;
                                default:
                                    // Only fail if this is actually a derived type we don't know about
                                    if (unlocker.GetType() != typeof(Unlocker))
                                    {
                                        Assert.Fail($"Unexpected unlocker type: {unlocker.GetType().Name}");
                                    }

                                    break;
                            }

                            break;
                        case UpgradeExtractor upgradeExtractor:
                            // Nothing specific to verify for UpgradeExtractor items
                            break;
                        case Currency currency:
                            // Nothing specific to verify for Currency items
                            break;
                        case AppearanceChanger appearanceChanger:
                            // Nothing specific to verify for AppearanceChanger items
                            break;
                        case RandomUnlocker randomUnlocker:
                            // Nothing specific to verify for RandomUnlocker items
                            break;
                        default:
                            Assert.Fail($"Unexpected consumable type: {consumable.GetType().Name}");
                            break;
                    }

                    break;
                case Weapon weapon:
                    Assert.True(weapon.DamageType.IsDefined());
                    Assert.True(weapon.MinPower >= 0);
                    Assert.True(weapon.MaxPower >= 0);
                    Assert.True(weapon.Defense >= 0);
                    Assert.All(weapon.Attributes, attribute => Assert.True(attribute.Value >= 1));
                    if (weapon.AttributeCombinationId.HasValue)
                    {
                        Assert.True(weapon.AttributeCombinationId >= 1);
                        Assert.Empty(weapon.StatChoices);
                    }
                    else if (weapon.StatChoices is not null)
                    {
                        Assert.Null(weapon.AttributeCombinationId);
                        Assert.Empty(weapon.Attributes);
                    }

                    break;
                case Backpack backItem:
                    Assert.All(backItem.Attributes, attribute => Assert.True(attribute.Value >= 1));
                    if (backItem.SuffixItemId.HasValue)
                    {
                        Assert.True(backItem.SuffixItemId.Value >= 1);
                    }

                    if (backItem.AttributeCombinationId.HasValue)
                    {
                        Assert.True(backItem.AttributeCombinationId >= 1);
                        Assert.Empty(backItem.StatChoices);
                    }
                    else if (backItem.StatChoices is not null)
                    {
                        Assert.Null(backItem.AttributeCombinationId);
                        Assert.Empty(backItem.Attributes);
                    }

                    Assert.All(backItem.UpgradesFrom, source =>
                    {
                        Assert.True(source.ItemId > 0);
                        Assert.True(source.Upgrade.IsDefined());
                    });
                    Assert.All(backItem.UpgradesInto, source =>
                    {
                        Assert.True(source.ItemId > 0);
                        Assert.True(source.Upgrade.IsDefined());
                    });
                    break;
                case Armor armor:
                    Assert.True(armor.WeightClass.IsDefined());
                    Assert.True(armor.Defense >= 0);
                    Assert.All(armor.Attributes, attribute => Assert.True(attribute.Value >= 1));
                    if (armor.SuffixItemId.HasValue)
                    {
                        Assert.True(armor.SuffixItemId.Value >= 1);
                    }

                    foreach (InfusionSlot? slot in armor.InfusionSlots)
                    {
                        Assert.NotNull(slot.Flags);
                        Assert.True(slot.Flags.Enrichment || slot.Flags.Infusion);
                        Assert.Empty(slot.Flags.Other);
                    }

                    if (armor.AttributeCombinationId.HasValue)
                    {
                        Assert.True(armor.AttributeCombinationId >= 1);
                        Assert.Empty(armor.StatChoices);
                    }
                    else if (armor.StatChoices.Count > 0)
                    {
                        Assert.Null(armor.AttributeCombinationId);
                        Assert.Empty(armor.Attributes);
                    }

                    break;
                case Trinket trinket:
                    if (trinket.AttributeCombinationId.HasValue)
                    {
                        Assert.True(trinket.AttributeCombinationId >= 1);
                        Assert.Empty(trinket.StatChoices);
                    }
                    else if (trinket.StatChoices is not null)
                    {
                        Assert.Null(trinket.AttributeCombinationId);
                        Assert.Empty(trinket.Attributes);
                    }

                    if (trinket is Ring ring)
                    {
                        Assert.All(ring.UpgradesFrom, source =>
                        {
                            Assert.True(source.ItemId > 0);
                            Assert.True(source.Upgrade.IsDefined());
                        });
                        Assert.All(ring.UpgradesInto, source =>
                        {
                            Assert.True(source.ItemId > 0);
                            Assert.True(source.Upgrade.IsDefined());
                        });
                    }

                    break;
                case SalvageTool salvageTool:
                    Assert.InRange(salvageTool.Charges, 1, 250);
                    break;
                case Miniature miniature:
                    Assert.True(miniature.MiniatureId >= 1);
                    break;
                case UpgradeComponent upgradeComponent:
                    Assert.Empty(upgradeComponent.UpgradeComponentFlags.Other);
                    Assert.Empty(upgradeComponent.InfusionUpgradeFlags.Other);
                    // There is a workaround in place for PvP runes and sigils not being classified as such
                    if (upgradeComponent.GameTypes.Contains(GameType.Pvp))
                    {
#if NET
                        if (upgradeComponent.Name.Contains("Rune", StringComparison.Ordinal))
#else
                        if (upgradeComponent.Name.Contains("Rune"))
#endif
                        {
                            Assert.IsType<Rune>(upgradeComponent);
                        }

#if NET
                        if (upgradeComponent.Name.Contains("Sigil", StringComparison.Ordinal))
#else
                        if (upgradeComponent.Name.Contains("Sigil"))
#endif
                        {
                            Assert.IsType<Sigil>(upgradeComponent);
                        }
                    }

                    break;
                case CraftingMaterial craftingMaterial:
                    Assert.All(craftingMaterial.UpgradesInto, source =>
                    {
                        Assert.True(source.ItemId > 0);
                        Assert.True(source.Upgrade.IsDefined());
                    });
                    break;
                case Trophy trophy:
                    // Nothing specific to verify for Trophy items
                    break;
                case Bag bag:
                    Assert.True(bag.Size >= 0);
                    // NoSellOrSort is a boolean, just verify it's not null
                    break;
                case Container container:
                    switch (container)
                    {
                        case ImmediateContainer immediateContainer:
                            // Nothing specific to verify for ImmediateContainer items
                            break;
                        case GiftBox giftBox:
                            // Nothing specific to verify for GiftBox items
                            break;
                        case BlackLionChest blackLionChest:
                            // Nothing specific to verify for BlackLionChest items
                            break;
                        default:
                            // Only fail if this is actually a derived type we don't know about
                            if (container.GetType() != typeof(Container))
                            {
                                Assert.Fail($"Unexpected container type: {container.GetType().Name}");
                            }

                            break;
                    }

                    break;
                case GatheringTool gatheringTool:
                    switch (gatheringTool)
                    {
                        case FishingRod fishingRod:
                            // Nothing specific to verify for FishingRod items
                            break;
                        case HarvestingSickle harvestingSickle:
                            // Nothing specific to verify for HarvestingSickle items
                            break;
                        case LoggingAxe loggingAxe:
                            // Nothing specific to verify for LoggingAxe items
                            break;
                        case MiningPick miningPick:
                            // Nothing specific to verify for MiningPick items
                            break;
                        case Bait bait:
                            // Nothing specific to verify for Bait items
                            break;
                        case Lure lure:
                            // Nothing specific to verify for Lure items
                            break;
                        default:
                            // Only fail if this is actually a derived type we don't know about
                            if (gatheringTool.GetType() != typeof(GatheringTool))
                            {
                                Assert.Fail($"Unexpected gathering tool type: {gatheringTool.GetType().Name}");
                            }

                            break;
                    }

                    break;
                case Gizmo gizmo:
                    switch (gizmo)
                    {
                        case BlackLionChestKey blackLionChestKey:
                            // Nothing specific to verify for BlackLionChestKey items
                            break;
                        case RentableContractNpc rentableContractNpc:
                            // Nothing specific to verify for RentableContractNpc items
                            break;
                        case UnlimitedConsumable unlimitedConsumable:
                            // Nothing specific to verify for UnlimitedConsumable items
                            break;
                        default:
                            // Base Gizmo case - validate common properties
                            if (gizmo.GuildUpgradeId.HasValue)
                            {
                                Assert.True(gizmo.GuildUpgradeId.Value > 0);
                            }

                            // Only fail if this is actually a derived type we don't know about
                            if (gizmo.GetType() != typeof(Gizmo))
                            {
                                Assert.Fail($"Unexpected gizmo type: {gizmo.GetType().Name}");
                            }

                            break;
                    }

                    break;
                case JadeTechModule jadeTechModule:
                    // Nothing specific to verify for JadeTechModule items
                    break;
                case PowerCore powerCore:
                    // Nothing specific to verify for PowerCore items
                    break;
                case Relic relic:
                    // Nothing specific to verify for Relic items
                    break;
                default:
                    Assert.Fail($"Unexpected item type: {actual.GetType().Name}");
                    break;
            }

            if (actual is IUpgradable upgradable)
            {
                Assert.Equal(upgradable.UpgradeSlotCount, upgradable.UpgradeSlots.Count);
                if (upgradable.UpgradeSlotCount > 0)
                {
                    Assert.Equal(upgradable.SuffixItemId, upgradable.UpgradeSlots[0]);
                }

                if (upgradable.UpgradeSlotCount > 1)
                {
                    Assert.Equal(upgradable.SecondarySuffixItemId, upgradable.UpgradeSlots[1]);
                }

                Assert.Equal(upgradable.InfusionSlotCount, upgradable.InfusionSlots.Count);
            }

            ItemLink chatLink = actual.GetChatLink();
            Assert.Equal(actual.ChatLink, chatLink.ToString());
            Assert.Equal(actual.Id, chatLink.ItemId);
            ItemLink chatLinkRoundtrip = ItemLink.Parse(chatLink.ToString());
            Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
        }
    }

    [Test]
    public async Task Can_be_serialized()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/items.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach (Item original in sut.Items.GetItemsBulk(cancellationToken: TestContext.Current!.CancellationToken).ValueOnly(TestContext.Current!.CancellationToken))
        {
            string json = JsonSerializer.Serialize(original);
            Item? roundTrip = JsonSerializer.Deserialize<Item>(json);
            Assert.IsType(original.GetType(), roundTrip);
            Assert.Equal(original, roundTrip);
        }
    }
}
