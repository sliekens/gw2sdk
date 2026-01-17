using System.Text.Json;

using GuildWars2.Hero.Equipment;
using GuildWars2.Hero.Equipment.Skiffs;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

[ServiceDataSource]
public class SkiffSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<SkiffSkin> actual, MessageContext context) = await sut.Hero.Equipment.Skiffs.GetSkiffSkins(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (SkiffSkin entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.IconUrl).IsNotNull();

                if (entry.IconUrl is not null)
                {
                    await Assert.That(entry.IconUrl.IsAbsoluteUri || entry.IconUrl.IsWellFormedOriginalString()).IsTrue();
                }

                await Assert.That(entry.DyeSlots).IsNotNull();

                foreach (DyeSlot dyeSlot in entry.DyeSlots)
                {
                    await Assert.That(dyeSlot.Material.IsDefined()).IsTrue();
                    await Assert.That(dyeSlot.ColorId).IsGreaterThan(0);
                }

#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.SkiffSkin);
                SkiffSkin? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.SkiffSkin);
#else
                string json = JsonSerializer.Serialize(entry);
                SkiffSkin? roundtrip = JsonSerializer.Deserialize<SkiffSkin>(json);
#endif
                await Assert.That(entry).IsEqualTo(roundtrip);
            }
        }
    }
}
