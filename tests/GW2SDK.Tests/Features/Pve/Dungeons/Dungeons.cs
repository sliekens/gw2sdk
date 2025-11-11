using GuildWars2.Pve.Dungeons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

[ServiceDataSource]
public class Dungeons(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Dungeon> actual, MessageContext context) = await sut.Pve.Dungeons.GetDungeons(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotEmpty(entry.Paths);
            Assert.All(entry.Paths, path =>
            {
                Assert.NotEmpty(path.Id);
                Assert.True(path.Kind.IsDefined());
            });
        });
    }
}
