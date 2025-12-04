using System.Text.Json;

using GuildWars2.Hero.Equipment;
using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountSkins(Gw2Client sut)
{
    [Test]
    public async Task Mount_skins_can_be_listed()
    {
        (HashSet<MountSkin> actual, MessageContext context) = await sut.Hero.Equipment.Mounts.GetMountSkins(
            cancellationToken: TestContext.Current!.Execution.CancellationToken
        );

        // https://github.com/gw2-api/issues/issues/134
        await Assert.That(context).Member(c => c.ResultCount, resultCount => resultCount.IsEqualTo(actual.Count + 1))
            .And.Member(c => c.ResultTotal, resultTotal => resultTotal.IsEqualTo(actual.Count + 1));
        using (Assert.Multiple())
        {
            foreach (MountSkin entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.IconUrl is not null && entry.IconUrl.IsAbsoluteUri).IsTrue();
                await Assert.That(entry.DyeSlots).IsNotEmpty();
                using (Assert.Multiple())
                {
                    foreach (DyeSlot slot in entry.DyeSlots)
                    {
                        await Assert.That(slot.Material.IsDefined()).IsTrue();
                        await Assert.That(slot.ColorId).IsGreaterThan(0);
                    }
                }
#pragma warning disable CS0618 // Type or member is obsolete
                await Assert.That(entry.Mount.IsDefined()).IsTrue();
#pragma warning restore CS0618 // Type or member is obsolete

#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.MountSkin);
                MountSkin? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.MountSkin);
#else
                string json = JsonSerializer.Serialize(entry);
                MountSkin? roundtrip = JsonSerializer.Deserialize<MountSkin>(json);
#endif
                await Assert.That(roundtrip).IsEqualTo(entry);
            }
        }
    }
}
