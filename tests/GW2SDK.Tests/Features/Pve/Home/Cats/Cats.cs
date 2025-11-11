using GuildWars2.Pve.Home.Cats;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

[ServiceDataSource]
public class Cats(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Cat> actual, MessageContext context) = await sut.Pve.Home.GetCats(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, cat =>
        {
            Assert.NotNull(cat);
            Assert.True(cat.Id > 0);
            Assert.NotEmpty(cat.Hint);
        });
    }
}
