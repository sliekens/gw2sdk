using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Items;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

[NotInParallel("Items")]
public class Items
{
    [Test]
    public async Task Can_be_enumerated()
    {
        using JsonLinesHttpMessageHandler handler = new("Data/items.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach ((Item actual, MessageContext context) in sut.Items.GetItemsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken))
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual)
                .Member(a => a.Id >= 1, m => m.IsTrue())
                .And.Member(a => a.Name, m => m.IsNotNull())
                .And.Member(a => a.Description, m => m.IsNotNull());
            MarkupSyntaxValidator.Validate(actual.Description);
            await Assert.That(actual)
                .Member(a => a.Rarity.IsDefined(), m => m.IsTrue())
                .And.Member(a => a.VendorValue.Amount >= 0, m => m.IsTrue())
                .And.Member(a => a.Level, m => m.IsBetween(0, 80))
                .And.Member(a => a.Restrictions.BodyTypes, m => m.IsNotEmpty())
                .And.Member(a => a.Restrictions.BodyTypes, m => m.All(type => type.IsDefined()))
                .And.Member(a => a.Restrictions.Races, m => m.IsNotEmpty())
                .And.Member(a => a.Restrictions.Races, m => m.All(race => race.IsDefined()))
                .And.Member(a => a.Restrictions.Professions, m => m.IsNotEmpty())
                .And.Member(a => a.Restrictions.Other, m => m.IsEmpty())
                .And.Member(a => a.Restrictions.Professions, m => m.All(profession => profession.IsDefined()))
                .And.Member(a => a.GameTypes, m => m.All(gameType => gameType.IsDefined()));
            switch (actual)
            {
                case Consumable consumable:
                    switch (consumable)
                    {
                        case Transmutation transmutation:
                            await Assert.That(transmutation.SkinIds).IsNotEmpty();
                            break;
                        case GenericConsumable genericConsumable:
                            if (genericConsumable.Effect is not null)
                            {
                                await Assert.That(genericConsumable.Effect)
                                    .Member(e => e.Name, m => m.IsNotNull())
                                    .And.Member(e => e.Description, m => m.IsNotNull())
                                    .And.Member(e => e.Duration >= TimeSpan.Zero, m => m.IsTrue())
                                    .And.Member(e => e.ApplyCount >= 0, m => m.IsTrue());
                            }
                            if (genericConsumable.GuildUpgradeId.HasValue)
                            {
                                await Assert.That(genericConsumable.GuildUpgradeId.Value > 0).IsTrue();
                            }
                            break;
                        case Utility utility:
                            if (utility.Effect is not null)
                            {
                                await Assert.That(utility.Effect)
                                    .Member(e => e.Name, m => m.IsNotNull())
                                    .And.Member(e => e.Description, m => m.IsNotNull())
                                    .And.Member(e => e.Duration >= TimeSpan.Zero, m => m.IsTrue())
                                    .And.Member(e => e.ApplyCount >= 0, m => m.IsTrue());
                            }
                            break;
                        case Food food:
                            if (food.Effect is not null)
                            {
                                await Assert.That(food.Effect)
                                    .Member(e => e.Name, m => m.IsNotNull())
                                    .And.Member(e => e.Description, m => m.IsNotNull())
                                    .And.Member(e => e.Duration >= TimeSpan.Zero, m => m.IsTrue())
                                    .And.Member(e => e.ApplyCount >= 0, m => m.IsTrue());
                            }
                            break;
                        case Service service:
                            if (service.Effect is not null)
                            {
                                await Assert.That(service.Effect)
                                    .Member(e => e.Name, m => m.IsNotNull())
                                    .And.Member(e => e.Description, m => m.IsNotNull())
                                    .And.Member(e => e.Duration >= TimeSpan.Zero, m => m.IsTrue())
                                    .And.Member(e => e.ApplyCount >= 0, m => m.IsTrue());
                            }
                            if (service.GuildUpgradeId.HasValue)
                            {
                                await Assert.That(service.GuildUpgradeId.Value > 0).IsTrue();
                            }
                            break;
                        case Booze:
                        case ContractNpc:
                        case HalloweenConsumable:
                        case MountLicense:
                        case TeleportToFriend:
                            break;
                        case Unlocker unlocker:
                            switch (unlocker)
                            {
                                case RecipeSheet recipeSheet:
                                    await Assert.That(recipeSheet.RecipeId > 0).IsTrue();
                                    await Assert.That(recipeSheet.ExtraRecipeIds).IsNotNull();
                                    foreach ((int extraRecipeId, RecipeLink? extraRecipeLink) in recipeSheet.ExtraRecipeIds.Zip(recipeSheet.GetExtraRecipeChatLinks(), (extraRecipeId, extraRecipeLink) => (extraRecipeId, extraRecipeLink)))
                                    {
                                        await Assert.That(extraRecipeLink!.RecipeId).IsEqualTo(extraRecipeId);
                                    }
                                    RecipeLink sheetRecipeLink = recipeSheet.GetRecipeChatLink();
                                    await Assert.That(sheetRecipeLink.RecipeId).IsEqualTo(recipeSheet.RecipeId);
                                    RecipeLink sheetRecipeLinkRoundtrip = RecipeLink.Parse(sheetRecipeLink.ToString());
                                    await Assert.That(sheetRecipeLinkRoundtrip.ToString()).IsEqualTo(sheetRecipeLink.ToString());
                                    break;
                                case Dye dye:
                                    await Assert.That(dye.ColorId > 0).IsTrue();
                                    break;
                                case BagSlotExpansion:
                                case BankTabExpansion:
                                case BuildStorageExpansion:
                                case BuildTemplateExpansion:
                                case ContentUnlocker:
                                case EquipmentTemplateExpansion:
                                case GliderSkinUnlocker:
                                case JadeBotSkinUnlocker:
                                case ConjuredDoorwayUnlocker:
                                case MiniatureUnlocker:
                                case MistChampionSkinUnlocker:
                                case MountSkinUnlocker:
                                case OutfitUnlocker:
                                case SharedInventorySlot:
                                case StorageExpander:
                                    break;
                                default:
                                    if (unlocker.GetType() != typeof(Unlocker))
                                    {
                                        await Assert.That(unlocker.GetType() != typeof(Unlocker)).IsFalse().Because($"Unexpected unlocker type: {unlocker.GetType().Name}");
                                    }
                                    break;
                            }
                            break;
                        case UpgradeExtractor:
                        case Currency:
                        case AppearanceChanger:
                        case RandomUnlocker:
                            break;
                        default:
                            await Assert.That(consumable.GetType() != typeof(Consumable)).IsFalse().Because($"Unexpected consumable type: {consumable.GetType().Name}");
                            break;
                    }
                    break;
                case Weapon weapon:
                    await Assert.That(weapon)
                        .Member(w => w.DamageType.IsDefined(), m => m.IsTrue())
                        .And.Member(w => w.MinPower >= 0, m => m.IsTrue())
                        .And.Member(w => w.MaxPower >= 0, m => m.IsTrue())
                        .And.Member(w => w.Defense >= 0, m => m.IsTrue())
                        .And.Member(w => w.Attributes, m => m.All(attribute => attribute.Value >= 1));
                    if (weapon.AttributeCombinationId.HasValue)
                    {
                        await Assert.That(weapon)
                            .Member(w => w.AttributeCombinationId >= 1, m => m.IsTrue())
                            .And.Member(w => w.StatChoices, m => m.IsEmpty());
                    }
                    else if (weapon.StatChoices is not null)
                    {
                        await Assert.That(weapon)
                            .Member(w => w.AttributeCombinationId, m => m.IsNull())
                            .And.Member(w => w.Attributes, m => m.IsEmpty());
                    }
                    break;
                case BackItem backItem:
                    await Assert.That(backItem.Attributes).All(attribute => attribute.Value >= 1);
                    if (backItem.SuffixItemId.HasValue)
                    {
                        await Assert.That(backItem.SuffixItemId.Value >= 1).IsTrue();
                    }
                    if (backItem.AttributeCombinationId.HasValue)
                    {
                        await Assert.That(backItem)
                            .Member(b => b.AttributeCombinationId >= 1, m => m.IsTrue())
                            .And.Member(b => b.StatChoices, m => m.IsEmpty());
                    }
                    else if (backItem.StatChoices is not null)
                    {
                        await Assert.That(backItem)
                            .Member(b => b.AttributeCombinationId, m => m.IsNull())
                            .And.Member(b => b.Attributes, m => m.IsEmpty());
                    }
                    await Assert.That(backItem)
                        .Member(b => b.UpgradesFrom, m => m.All(source => source.ItemId > 0 && source.Upgrade.IsDefined()))
                        .And.Member(b => b.UpgradesInto, m => m.All(source => source.ItemId > 0 && source.Upgrade.IsDefined()));
                    break;
                case Armor armor:
                    await Assert.That(armor)
                        .Member(a => a.WeightClass.IsDefined(), m => m.IsTrue())
                        .And.Member(a => a.Defense >= 0, m => m.IsTrue())
                        .And.Member(a => a.Attributes, m => m.All(attribute => attribute.Value >= 1));
                    if (armor.SuffixItemId.HasValue)
                    {
                        await Assert.That(armor.SuffixItemId.Value >= 1).IsTrue();
                    }
                    foreach (InfusionSlot? slot in armor.InfusionSlots)
                    {
                        await Assert.That(slot!)
                            .Member(s => s.Flags, m => m.IsNotNull())
                            .And.Member(s => s.Flags.Enrichment || s.Flags.Infusion, m => m.IsTrue())
                            .And.Member(s => s.Flags.Other, m => m.IsEmpty());
                    }
                    if (armor.AttributeCombinationId.HasValue)
                    {
                        await Assert.That(armor)
                            .Member(a => a.AttributeCombinationId >= 1, m => m.IsTrue())
                            .And.Member(a => a.StatChoices, m => m.IsEmpty());
                    }
                    else if (armor.StatChoices.Count > 0)
                    {
                        await Assert.That(armor)
                            .Member(a => a.AttributeCombinationId, m => m.IsNull())
                            .And.Member(a => a.Attributes, m => m.IsEmpty());
                    }
                    break;
                case Trinket trinket:
                    if (trinket.AttributeCombinationId.HasValue)
                    {
                        await Assert.That(trinket)
                            .Member(t => t.AttributeCombinationId >= 1, m => m.IsTrue())
                            .And.Member(t => t.StatChoices, m => m.IsEmpty());
                    }
                    else if (trinket.StatChoices is not null)
                    {
                        await Assert.That(trinket)
                            .Member(t => t.AttributeCombinationId, m => m.IsNull())
                            .And.Member(t => t.Attributes, m => m.IsEmpty());
                    }
                    if (trinket is Ring ring)
                    {
                        await Assert.That(ring)
                            .Member(r => r.UpgradesFrom, m => m.All(source => source.ItemId > 0 && source.Upgrade.IsDefined()))
                            .And.Member(r => r.UpgradesInto, m => m.All(source => source.ItemId > 0 && source.Upgrade.IsDefined()));
                    }
                    break;
                case SalvageTool salvageTool:
                    await Assert.That(salvageTool.Charges).IsBetween(1, 250);
                    break;
                case MiniatureItem miniature:
                    await Assert.That(miniature.MiniatureId >= 1).IsTrue();
                    break;
                case UpgradeComponent upgradeComponent:
                    await Assert.That(upgradeComponent)
                        .Member(u => u.UpgradeComponentFlags.Other, m => m.IsEmpty())
                        .And.Member(u => u.InfusionUpgradeFlags.Other, m => m.IsEmpty());
                    if (upgradeComponent.GameTypes.Contains(GameType.Pvp))
                    {
#if NET
                        if (upgradeComponent.Name.Contains("Rune", StringComparison.Ordinal))
#else
                        if (upgradeComponent.Name.Contains("Rune"))
#endif
                        {
                            await Assert.That(upgradeComponent).IsTypeOf<Rune>();
                        }
#if NET
                        if (upgradeComponent.Name.Contains("Sigil", StringComparison.Ordinal))
#else
                        if (upgradeComponent.Name.Contains("Sigil"))
#endif
                        {
                            await Assert.That(upgradeComponent).IsTypeOf<Sigil>();
                        }
                    }
                    break;
                case CraftingMaterial craftingMaterial:
                    await Assert.That(craftingMaterial.UpgradesInto).All(source => source.ItemId > 0 && source.Upgrade.IsDefined());
                    break;
                case Trophy:
                    break;
                case Bag bag:
                    await Assert.That(bag.Size >= 0).IsTrue();
                    break;
                case Container container:
                    switch (container)
                    {
                        case ImmediateContainer:
                        case GiftBox:
                        case BlackLionChest:
                            break;
                        default:
                            if (container.GetType() != typeof(Container))
                            {
                                await Assert.That(container.GetType() != typeof(Container)).IsFalse().Because($"Unexpected container type: {container.GetType().Name}");
                            }
                            break;
                    }
                    break;
                case GatheringTool gatheringTool:
                    switch (gatheringTool)
                    {
                        case FishingRod:
                        case HarvestingSickle:
                        case LoggingAxe:
                        case MiningPick:
                        case Bait:
                        case Lure:
                            break;
                        default:
                            if (gatheringTool.GetType() != typeof(GatheringTool))
                            {
                                await Assert.That(gatheringTool.GetType() != typeof(GatheringTool)).IsFalse().Because($"Unexpected gathering tool type: {gatheringTool.GetType().Name}");
                            }
                            break;
                    }
                    break;
                case Gizmo gizmo:
                    switch (gizmo)
                    {
                        case BlackLionChestKey:
                        case RentableContractNpc:
                        case UnlimitedConsumable:
                            break;
                        default:
                            if (gizmo.GuildUpgradeId.HasValue)
                            {
                                await Assert.That(gizmo.GuildUpgradeId.Value > 0).IsTrue();
                            }
                            if (gizmo.GetType() != typeof(Gizmo))
                            {
                                await Assert.That(gizmo.GetType() != typeof(Gizmo)).IsFalse().Because($"Unexpected gizmo type: {gizmo.GetType().Name}");
                            }
                            break;
                    }
                    break;
                case JadeTechModule:
                case PowerCore:
                case Relic:
                    break;
                default:
                    await Assert.That(actual.GetType() != typeof(Item)).IsFalse().Because($"Unexpected item type: {actual.GetType().Name}");
                    break;
            }
            if (actual is IUpgradable upgradable)
            {
                await Assert.That(upgradable)
                    .Member(u => u.UpgradeSlotCount, m => m.IsEqualTo(upgradable.UpgradeSlots.Count));
                if (upgradable.UpgradeSlotCount > 0)
                {
                    await Assert.That(upgradable.SuffixItemId).IsEqualTo(upgradable.UpgradeSlots[0]);
                }
                if (upgradable.UpgradeSlotCount > 1)
                {
                    await Assert.That(upgradable.SecondarySuffixItemId).IsEqualTo(upgradable.UpgradeSlots[1]);
                }
                await Assert.That(upgradable.InfusionSlotCount).IsEqualTo(upgradable.InfusionSlots.Count);
            }
            ItemLink chatLink = actual.GetChatLink();
            await Assert.That(chatLink)
                .Member(c => c.ToString(), m => m.IsEqualTo(actual.ChatLink))
                .And.Member(c => c.ItemId, m => m.IsEqualTo(actual.Id));
            ItemLink chatLinkRoundtrip = ItemLink.Parse(chatLink.ToString());
            await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
        }
    }

    [Test]
    public async Task Can_be_serialized()
    {
        using JsonLinesHttpMessageHandler handler = new("Data/items.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach (Item original in sut.Items.GetItemsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken).ValueOnly(TestContext.Current!.Execution.CancellationToken))
        {
#if NET
            string json = JsonSerializer.Serialize(original, Common.TestJsonContext.Default.Item);
            Item? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Item);
#else
            string json = JsonSerializer.Serialize(original);
            Item? roundTrip = JsonSerializer.Deserialize<Item>(json);
#endif
            await Assert.That(roundTrip).IsNotNull()
                .And.Member(r => r!.GetType(), t => t.IsEqualTo(original.GetType()))
                .And.IsEqualTo(original);
        }
    }
}
