using System.Text.Json;

using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountSkins
{
    [Fact]
    public async Task Mount_skins_can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<MountSkin> actual, MessageContext context) = await sut.Hero.Equipment.Mounts.GetMountSkins(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotEmpty(entry.Name);
                Assert.True(entry.IconUrl is not null && entry.IconUrl.IsAbsoluteUri);
                Assert.NotEmpty(entry.DyeSlots);
                Assert.All(
                    entry.DyeSlots,
                    slot =>
                    {
                        Assert.True(slot.Material.IsDefined());
                        Assert.True(slot.ColorId > 0);
                    }
                );

                Assert.True(entry.Mount.IsDefined());

                string json = JsonSerializer.Serialize(entry);
                MountSkin? roundtrip = JsonSerializer.Deserialize<MountSkin>(json);
                Assert.Equal(entry, roundtrip);
            }
        );
    }
}
