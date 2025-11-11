using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_name()
    {
        HashSet<MountName> names = [MountName.Raptor, MountName.Jackal, MountName.Skimmer];
        (HashSet<Mount> actual, MessageContext context) = await sut.Hero.Equipment.Mounts.GetMountsByNames(names, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(names.Count, context.ResultCount);
        Assert.True(context.ResultTotal > names.Count);
        Assert.Equal(names.Count, actual.Count);
        Assert.Collection(names, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
