using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountNames
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Extensible<MountName>> actual, _) = await sut.Hero.Equipment.Mounts.GetMountNames(TestContext.Current!.CancellationToken);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.IsDefined());
        });
    }
}
