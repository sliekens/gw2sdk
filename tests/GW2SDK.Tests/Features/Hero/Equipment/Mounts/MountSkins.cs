using System.Text.Json;

using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountSkins
{
    [Test]
    public async Task Mount_skins_can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<MountSkin> actual, MessageContext context) = await sut.Hero.Equipment.Mounts.GetMountSkins(
            cancellationToken: TestContext.Current!.Execution.CancellationToken
        );

        // https://github.com/gw2-api/issues/issues/134
        Assert.Equal(context.ResultCount, actual.Count + 1);
        Assert.Equal(context.ResultTotal, actual.Count + 1);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.True(entry.IconUrl is not null && entry.IconUrl.IsAbsoluteUri);
            Assert.NotEmpty(entry.DyeSlots);
            Assert.All(entry.DyeSlots, slot =>
            {
                Assert.True(slot.Material.IsDefined());
                Assert.True(slot.ColorId > 0);
            });
#pragma warning disable CS0618 // Type or member is obsolete
            Assert.True(entry.Mount.IsDefined());
#pragma warning restore CS0618 // Type or member is obsolete

            string json = JsonSerializer.Serialize(entry);
            MountSkin? roundtrip = JsonSerializer.Deserialize<MountSkin>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
