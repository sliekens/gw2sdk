using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Items;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

public class Items
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using var httpClient =
            new HttpClient(new JsonLinesHttpMessageHandler("Data/items.jsonl.gz"));
        var sut = new Gw2Client(httpClient);
        await foreach (var (actual, context) in sut.Items.GetItemsBulk(
                cancellationToken: TestContext.Current.CancellationToken
            ))
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
            Assert.All(
                actual.Restrictions.Professions,
                profession => Assert.True(profession.IsDefined())
            );
            Assert.All(actual.GameTypes, gameType => Assert.True(gameType.IsDefined()));
            switch (actual)
            {
                case Consumable consumable:
                    switch (consumable)
                    {
                        case Transmutation transmutation:
                            Assert.NotEmpty(transmutation.SkinIds);
                            break;
                        case RecipeSheet recipe:
                            Assert.True(recipe.Id > 0);
                            Assert.NotNull(recipe.ExtraRecipeIds);
                            foreach (var (extraRecipeId, extraRecipeLink) in
                                recipe.ExtraRecipeIds.Zip(
                                    recipe.GetExtraRecipeChatLinks(),
                                    (extraRecipeId, extraRecipeLink) =>
                                        (extraRecipeId, extraRecipeLink)
                                ))
                            {
                                Assert.Equal(extraRecipeId, extraRecipeLink.RecipeId);
                            }

                            var recipeLink = recipe.GetRecipeChatLink();
                            Assert.Equal(recipe.RecipeId, recipeLink.RecipeId);

                            var recipeLinkRoundtrip = RecipeLink.Parse(recipeLink.ToString());
                            Assert.Equal(recipeLink.ToString(), recipeLinkRoundtrip.ToString());

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

                    Assert.All(
                        backItem.UpgradesFrom,
                        source =>
                        {
                            Assert.True(source.ItemId > 0);
                            Assert.True(source.Upgrade.IsDefined());
                        }
                    );

                    Assert.All(
                        backItem.UpgradesInto,
                        source =>
                        {
                            Assert.True(source.ItemId > 0);
                            Assert.True(source.Upgrade.IsDefined());
                        }
                    );

                    break;
                case Armor armor:
                    Assert.True(armor.WeightClass.IsDefined());
                    Assert.True(armor.Defense >= 0);
                    Assert.All(armor.Attributes, attribute => Assert.True(attribute.Value >= 1));
                    if (armor.SuffixItemId.HasValue)
                    {
                        Assert.True(armor.SuffixItemId.Value >= 1);
                    }

                    foreach (var slot in armor.InfusionSlots)
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
                        Assert.All(
                            ring.UpgradesFrom,
                            source =>
                            {
                                Assert.True(source.ItemId > 0);
                                Assert.True(source.Upgrade.IsDefined());
                            }
                        );

                        Assert.All(
                            ring.UpgradesInto,
                            source =>
                            {
                                Assert.True(source.ItemId > 0);
                                Assert.True(source.Upgrade.IsDefined());
                            }
                        );
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
                        if (upgradeComponent.Name.Contains("Rune"))
                        {
                            Assert.IsType<Rune>(upgradeComponent);
                        }

                        if (upgradeComponent.Name.Contains("Sigil"))
                        {
                            Assert.IsType<Sigil>(upgradeComponent);
                        }
                    }

                    break;
                case CraftingMaterial craftingMaterial:
                    Assert.All(
                        craftingMaterial.UpgradesInto,
                        source =>
                        {
                            Assert.True(source.ItemId > 0);
                            Assert.True(source.Upgrade.IsDefined());
                        }
                    );
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

            var chatLink = actual.GetChatLink();
            Assert.Equal(actual.ChatLink, chatLink.ToString());
            Assert.Equal(actual.Id, chatLink.ItemId);

            var chatLinkRoundtrip = ItemLink.Parse(chatLink.ToString());
            Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
        }
    }

    [Fact]
    public async Task Can_be_serialized()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using var httpClient =
            new HttpClient(new JsonLinesHttpMessageHandler("Data/items.jsonl.gz"));
        var sut = new Gw2Client(httpClient);
        await foreach (var original in sut.Items
            .GetItemsBulk(cancellationToken: TestContext.Current.CancellationToken)
            .ValueOnly(TestContext.Current.CancellationToken))
        {
            var json = JsonSerializer.Serialize(original);
            var roundTrip = JsonSerializer.Deserialize<Item>(json);
            Assert.IsType(original.GetType(), roundTrip);
            Assert.Equal(original, roundTrip);
        }
    }
}
