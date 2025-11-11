using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountNames(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Extensible<MountName>> actual, _) = await sut.Hero.Equipment.Mounts.GetMountNames(TestContext.Current!.Execution.CancellationToken);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.IsDefined());
        });
    }
}
