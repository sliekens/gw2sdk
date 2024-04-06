using System.Text.Json;
using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class SkinJson(SkinFixture fixture) : IClassFixture<SkinFixture>
{
    [Fact]
    public void Skins_can_be_created_from_json() =>
        Assert.All(
            fixture.Skins,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetEquipmentSkin(MissingMemberBehavior.Error);
                var link = actual.GetChatLink();

                Assert.True(actual.Id > 0);
                Assert.NotEmpty(actual.Races);
                Assert.All(actual.Races, race => Assert.True(race.IsDefined()));
                Assert.Equal(actual.Id, link.SkinId);
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
            }
        );
}
