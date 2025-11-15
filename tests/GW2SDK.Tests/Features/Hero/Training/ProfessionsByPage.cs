using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Training;

[ServiceDataSource]
public class ProfessionsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (HashSet<Profession> actual, MessageContext context) = await sut.Hero.Training.GetProfessionsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(pageSize, context.ResultCount);
        Assert.True(context.PageTotal > 0);
        Assert.True(context.ResultTotal > 0);
        Assert.Equal(pageSize, actual.Count);
        Assert.All(actual, Assert.NotNull);
    }
}
