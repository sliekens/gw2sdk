using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Hero;
using GuildWars2.Hero.Equipment;
using GuildWars2.Hero.Equipment.Wardrobe;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

[NotInParallel("Skins")]
public class Skins
{
    [Test]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/skins.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach ((EquipmentSkin actual, MessageContext context) in sut.Hero.Equipment.Wardrobe.GetSkinsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken))
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id > 0).IsTrue();
            await Assert.That(actual.Name).IsNotNull();
            await Assert.That(actual.Description).IsNotNull();
            MarkupSyntaxValidator.Validate(actual.Description);
            await Assert.That(actual.Races).IsNotEmpty();
            foreach (Extensible<RaceName> race in actual.Races)
            {
                await Assert.That(race.IsDefined()).IsTrue();
            }
            await Assert.That(actual.Rarity.IsDefined()).IsTrue();
            if (actual is ArmorSkin armor)
            {
                await Assert.That(armor.WeightClass.IsDefined()).IsTrue();
                foreach (DyeSlot? slot in armor.DyeSlots?.Default ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.AsuraFemale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.AsuraMale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.CharrFemale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.CharrMale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.HumanFemale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.HumanMale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.NornFemale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.NornMale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.SylvariFemale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
                foreach (DyeSlot? slot in armor.DyeSlots?.SylvariMale ?? Enumerable.Empty<DyeSlot?>())
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.ColorId > 0).IsTrue();
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                    }
                }
            }
            else if (actual is WeaponSkin weapon)
            {
                await Assert.That(weapon.DamageType.IsDefined()).IsTrue();
            }

            SkinLink chatLink = actual.GetChatLink();
            await Assert.That(chatLink.SkinId).IsEqualTo(actual.Id);
            SkinLink chatLinkRoundtrip = SkinLink.Parse(chatLink.ToString());
            await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
        }
    }

    [Test]
    public async Task Can_be_serialized()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/skins.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach (EquipmentSkin original in sut.Hero.Equipment.Wardrobe.GetSkinsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken).ValueOnly(TestContext.Current!.Execution.CancellationToken))
        {
#if NET
            string json = JsonSerializer.Serialize(original, Common.TestJsonContext.Default.EquipmentSkin);
            EquipmentSkin? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.EquipmentSkin);
#else
            string json = JsonSerializer.Serialize(original);
            EquipmentSkin? roundTrip = JsonSerializer.Deserialize<EquipmentSkin>(json);
#endif
            await Assert.That(roundTrip).IsNotNull()
                .And.IsOfType(original.GetType())
                .And.IsEqualTo(original);
            await Assert.That(roundTrip).IsEqualTo(original);
        }
    }
}
