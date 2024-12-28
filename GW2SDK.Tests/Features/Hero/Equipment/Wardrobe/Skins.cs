using GuildWars2.Chat;
using GuildWars2.Hero.Equipment.Wardrobe;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class Skins
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using var httpClient =
            new HttpClient(new JsonLinesHttpMessageHandler("Data/skins.jsonl.gz"));
        var sut = new Gw2Client(httpClient);
        await foreach (var (actual, context) in sut.Hero.Equipment.Wardrobe.GetSkinsBulk(
                cancellationToken: TestContext.Current.CancellationToken
            ))
        {
            Assert.NotNull(context);
            Assert.True(actual.Id > 0);
            Assert.NotNull(actual.Name);
            Assert.NotNull(actual.Description);
            MarkupSyntaxValidator.Validate(actual.Description);
            Assert.NotEmpty(actual.Races);
            Assert.All(actual.Races, race => Assert.True(race.IsDefined()));
            Assert.True(actual.Rarity.IsDefined());

            if (actual is ArmorSkin armor)
            {
                Assert.True(armor.WeightClass.IsDefined());
                Assert.All(
                    armor.DyeSlots?.Default ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.AsuraFemale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.AsuraMale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.CharrFemale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.CharrMale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.HumanFemale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.HumanMale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.NornFemale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.NornMale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.SylvariFemale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
                Assert.All(
                    armor.DyeSlots?.SylvariMale ?? [],
                    slot =>
                    {
                        if (slot is not null)
                        {
                            Assert.True(slot.ColorId > 0);
                            Assert.True(slot.Material.IsDefined());
                        }
                    }
                );
            }
            else if (actual is WeaponSkin weapon)
            {
                Assert.True(weapon.DamageType.IsDefined());
            }

            var chatLink = actual.GetChatLink();
            Assert.Equal(actual.Id, chatLink.SkinId);

            var chatLinkRoundtrip = SkinLink.Parse(chatLink.ToString());
            Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
        }
    }
}
